namespace microkart.catalog
{
    public class AwsConfigOptions
    {
        public const string AwsConfig = "AwsConfig";

        public string Accesskey { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty ;
        public string S3Url { get; set; }
    }
}
