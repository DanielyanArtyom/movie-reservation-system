namespace MovieReservation.API.Extensions;

public static class HttpResponseExtension
{
    public static void SetNoCacheHeaders(this HttpResponse response)
    {
        response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
        response.Headers["Pragma"] = "no-cache";
        response.Headers["Expires"] = "0";
    }
}