using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Runtime.InteropServices.ComTypes;

namespace Crawler.Lib
{
	/// <summary>
	/// Simple http server which is used to serve the crawled files under their uri.
	/// </summary>
	public class HttpServer
	{
		public Uri Uri {
			get;
			private set;
		}

		bool _stop = false;

		Thread _dispatcherThread;

		HttpListener _listener = new HttpListener ();

		public HttpServer (Uri uri)
		{
			Uri = uri;

			_listener.Prefixes.Add (uri.ToString ());
		}

		/// <summary>
		/// Starts the server.
		/// </summary>
		public void Start ()
		{
			_stop = false;
			_listener.Start ();

			_dispatcherThread = new Thread (new ThreadStart (Accept));
			_dispatcherThread.Start ();
		}

		/// <summary>
		/// Stops the server.
		/// </summary>
		public void Stop ()
		{
			_stop = true;
			_listener.Stop ();

			_dispatcherThread.Abort ();
		}

		/// <summary>
		/// Listens for incoming requests and hands them over to the dispatcher.
		/// </summary>
		void Accept ()
		{
			while (!_stop) {
				Dispatch (_listener.GetContext ());
			}
		}

		/// <summary>
		/// Dispatches the request to a worker.
		/// </summary>
		void Dispatch (HttpListenerContext context)
		{
			ThreadPool.QueueUserWorkItem (_ => {
				var handler = Request;
				if (handler != null)
					handler(this, new RequestEventArgs(context));
			});
		}

		public event EventHandler<RequestEventArgs> Request;
	}

	public class RequestEventArgs : EventArgs {
		public HttpListenerContext Context {
			get;
			private set;
		}

		public RequestEventArgs (HttpListenerContext context)
		{
			Context = context;
		}
	}
}