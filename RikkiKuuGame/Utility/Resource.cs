/*
Utility Resource Functions.
*/

namespace RikkiKuu;

static partial class Utility
{
	
	// asynchronous resource load with progress fraction callback & error message callback
	public static async Task<Resource?> AsyncLoad (string target, Action<float>? on_progress = null, Action<string>? on_error = null)
	{
		
		// try to initiate the resource load operation
		var req_status = ResourceLoader.LoadThreadedRequest( target );
		
		// catch-all for primary issues, like file missing or unknown extension etc
		if (req_status != Error.Ok) {
			on_error?.Invoke( $@"Resource Error [{target}] -> {req_status}" );
			return null;
		}
		
		// assume all is going well and we will get a result
		Resource? result = null;
		
		// progress is sent back as a variant in an unspecialized array
		GC.Array progress_array = new();
		
		// fire initial load status event
		on_progress?.Invoke( 0f );
		
		// poll an update on the status of the load operation
		for (var loading = true; loading;) switch (ResourceLoader.LoadThreadedGetStatus( target, progress_array )) {
			
			// resource is still loading
			case ResourceLoader.ThreadLoadStatus.InProgress: {
				
				// fire mid-load status event
				on_progress?.Invoke( progress_array[0].AsSingle() );
				
				// wait for the situation to change perhaps
				await NextProcessFrame();
				continue;
			}
			
			// resource loaded successfully 
			case ResourceLoader.ThreadLoadStatus.Loaded: {
				
				// fetch result of load operation
				result = ResourceLoader.LoadThreadedGet( target );
				
				// fire final load status event
				on_progress?.Invoke( 1f );
				
				// exit loading loop
				loading = false;
				continue;
			}
			
			// resource failed to load
			case ResourceLoader.ThreadLoadStatus.Failed: {
				
				// fire off error message & return now
				on_error?.Invoke( $@"Failed to Load" );
				return null;
			}
			
			// resource hasn't been loaded this way or doesn't exist
			case ResourceLoader.ThreadLoadStatus.InvalidResource: {
				
				// fire off error message & return now
				on_error?.Invoke( $@"Invalid Resource" );
				return null;
			}
		}
		
		return result;
	}
	
}