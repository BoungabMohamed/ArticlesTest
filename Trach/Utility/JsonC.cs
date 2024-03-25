using System.Text.Json;

namespace Trach.Utility
{

    public class JsonC
    {
        public static List<ContentBlock> GetContentBlocks(string jsonContent)
        {
            var blocks = new List<ContentBlock>();

            try
            {
                var jsonDocument = JsonDocument.Parse(jsonContent);

                if (jsonDocument.RootElement.TryGetProperty("blocks", out var blocksElement) && blocksElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var block in blocksElement.EnumerateArray())
                    {
                        var type = block.TryGetProperty("type", out var typeElement) ? typeElement.GetString() : null;
                        var content = block.TryGetProperty("content", out var contentElement) ? contentElement.GetString() : null;

                        if (type != null && content != null)
                        {
                            blocks.Add(new ContentBlock
                            {
                                Type = type,
                                Content = content
                            });
                        }
                    }
                }
            }
            catch (JsonException) // In case of parsing errors
            {
                // Handle the error as appropriate for your application
            }

            return blocks;
        }

    }
}
