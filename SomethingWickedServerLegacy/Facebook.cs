using System.Net.Http;
using System.Threading.Tasks;

namespace SomethingWickedServerLegacy
{
    public class Facebook
    {
        private static string accessToken = "EAACYLTuTmpkBAPyt94AWJslw1zHKi9T0M7dRNFD9AYFmZAw7j9ZBteocetAkS2fS1vQdfVpY7PWjIr30xf0xxel1mSXlOVjdIYccsPKrO8h6P8t7Nle9cJ3VE8xRLoTX7mDJcduldshoRgltbDBA1E9q50RacuzFRREcmmtAZDZD";
        private static string graphApiUrl = "https://graph.facebook.com/v2.10/";

        public static async Task<string> GetContent(string node, string fields)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(graphApiUrl + node + "/" + "?fields=" + fields + "&access_token=" + accessToken))
                {
                    using (HttpContent content = response.Content)
                    {
                        return await content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}

public struct FacebookData
{
    public Album albums;
    public VideoList video_lists;
}

public struct Album
{
    public AlbumData[] data;
}

public struct AlbumData
{
    public string name;
    public string type;
    public string id;
    public Picture picture;

}

public struct Picture
{
    public PictureData data;
}

public struct PictureData
{
    public string url;
}

public struct VideoList
{
    public VideoListData[] data;
}

public struct VideoListData
{
    public string title;
    public string thumbnail;
    public string id;
}