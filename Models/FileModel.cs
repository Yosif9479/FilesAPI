namespace FilesAPI.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] Data { get; set; }

        public FileModel() 
        {
            Name = "Unnamed";
            Extension = ".txt";
            Data = new byte[0];
        }

        public FileModel(string name, string extension, byte[] data)
        {
            Name = name;
            Extension = extension;
            Data = data;
        }
    }
}
