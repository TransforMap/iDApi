using System;
using System.Linq;
using System.Net;
using Crawler.Lib;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using iDApi.JsonModel.iD;
using System.Dynamic;

namespace iDApi
{
	public class EditorChangeset {

		public bool IsPoint {
			get;
			set;
		}

		public bool IsPolygon {
			get;
			set;
		}

		public bool IsClosed {
			get;
			set;
		}

		public Dictionary<string,string> Tags {
			get;
			private set;
		}

		public HashSet<string> Nodes {
			get;
			private set;
		}

		public EditorChangeset ()
		{
			Tags = new Dictionary<string,string> ();
			Nodes = new HashSet<string> ();
		}
	}

	public class MainClass
	{
		public static void Main (string[] args)
		{
			HttpServer server = new HttpServer (new Uri("http://localhost:8023"));
			server.Start ();
			server.Request += HandleRequest;
		}

		static void HandleRequest (object sender, RequestEventArgs e)
		{
			var request = e.Context.Request;
			var response = e.Context.Response;
			var outStream = response.OutputStream;
			var outStreamWriter = new StreamWriter (outStream);

			var method = request.HttpMethod.ToLower ();
			var url = request.Url.ToString();

			var getObjectsPattern = new Regex ("http://.*/objects/(?'objectId'.*)");

			switch (method) {
			case "head":

				break;
			case "get":

				var m = getObjectsPattern.Match (url);
				if (!m.Success)
 					throw new Exception ("Invalid request");

				var objectId = m.Groups ["objectId"].Value;
				var documentUri = new Uri (string.Format ("https://edit.transformap.co/admin_party/{0}", objectId));

				var idOsmData = GetCouchDocument (documentUri);

				if (idOsmData.created != null) 
				{
					var osmObject = ParseChangeset (idOsmData);
				} 
				else if (idOsmData.modified != null) 
				{

				} 
				else if (idOsmData.deleted != null) 
				{

				}


				break;
			case "post":

				break;
			case "put":

				break;
			}

			outStreamWriter.Flush ();
			outStream.Close ();
		}

		static EditorChangeset ParseChangeset (dynamic idOsmData)
		{
			EditorChangeset osmObject = new EditorChangeset ();

			changeset chng = new changeset ();

			foreach (var item in idOsmData.created) {
				if (chng.created == null)
					chng.created = new List<item> ();
			}

			foreach (var item in idOsmData.modified) {
				if (chng.modified == null)
					chng.modified = new List<item> ();
			}

			foreach (var item in idOsmData.deleted) {
				if (chng.modified == null)
					chng.deleted = new List<item> ();
			}

			var itemParser = new Func<JObject, item> (o => {
				item i = new item();

				i.id = o.id;
				i.visible = o.visible;

				var loc = o.Property("loc");
				var nodes = o.Property("nodes");
				var tags = o.Property("tags");

				if (loc == null && nodes != null) {
					i.v = o.v;
				}

				if (nodes == null && loc != null) {
					loc l = new loc(){
						lat = loc.Value,
						lon = loc.Value
					};
				}
			});

			// Find the 'nodes' property which contains information 
			// about the polygon (if item is not a point)
			var nodesProp = item.Property ("nodes");
			if (nodesProp != null) {
				if (osmObject.IsPoint) {
					osmObject.IsPoint = false;
					osmObject.IsPolygon = true;
				}
				// Determine if the submitted object is a closed figure
				foreach (var node in nodesProp.Value) {
					if (osmObject.Nodes.Contains (node.Value)) {
						osmObject.IsClosed = true;
						continue;
					}
					osmObject.Nodes.Add (node.Value);
				}
			}

			var tagsProp = item.Property ("tags");
			if (tagsProp != null) {
				JObject tagKeyValuePairs = tagsProp.Value;
				// Concat the values of duplicate keys to a comma seperated list of values
				foreach (var tag in tagKeyValuePairs.Properties ()) {
					osmObject.Tags.Add (tag.Name, tag.Value.ToString ());
				}
			}

			return osmObject;
		}

		static dynamic GetCouchDocument (Uri documentUri) {
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (documentUri);
			var response = request.GetResponse ();
			using (StreamReader sr = new StreamReader (response.GetResponseStream ())) {
				return JObject.Parse(sr.ReadToEnd());
			}
		}
	}
}