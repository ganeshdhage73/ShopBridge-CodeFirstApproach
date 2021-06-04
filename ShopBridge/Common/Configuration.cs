using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ShopBridge.Common
{
    public class Configuration
    {
        private IConfiguration config { get; set; }
        public Configuration(IConfiguration configuration)
        {
            config = configuration;
        }

        public string DbConnection => config.GetSection("ConnectionStrings:DbConnection").Value;

        /* START - Email Configuration Section */
        public string EmailAccountAddress => config["EmailConfiguration:AccountAddress"];
        public string EmailFromEmailAddress => config["EmailConfiguration:FromEmailAddress"];
        public string EmailReplyToEmailAddress => config["EmailConfiguration:ReplyToEmailAddress"];
        public string EmailHostPassword => config["EmailConfiguration:HostPassword"];
        public string EmailHost => config["EmailConfiguration:Host"];
        public int EmailPort => config.GetValue<Int32>("EmailConfiguration:Port");
        public bool SendEmail => config.GetValue<bool>("EmailConfiguration:SendEmail");

        /* END - Email Configuration Section */

        /*START - CORS Policy Setup*/
        public bool AllowAnyHeader => config.GetValue<bool>("CorsPolicyConfig:AllowAnyHeader");
        public bool AllowAnyHeaders => config.GetValue<bool>("CorsPolicyConfig:AllowAnyHeader");
        public bool AllowAnyMethod => config.GetValue<bool>("CorsPolicyConfig:AllowAnyMethod");
        public bool AllowAnyOrigin => config.GetValue<bool>("CorsPolicyConfig:AllowAnyOrigin");
        public bool AllowAnyCredential => config.GetValue<bool>("CorsPolicyConfig:AllowAnyCredential");

        public List<String> WithExposedHeaders
        {
            get
            {
                var list = new List<string>();
                config.GetSection("CorsPolicyConfig:WithExposedHeaders").Bind(list);
                return list;
            }
        }

        public List<string> WithHeaders
        {
            get
            {
                var list = new List<string>();
                config.GetSection("CorsPolicyConfig:WithHeaders").Bind(list);
                return list;
            }
        }

        public List<string> WithMethods
        {
            get
            {
                var list = new List<string>();
                config.GetSection("CorsPolicyConfig:WithMethods").Bind(list);
                return list;
            }
        }

        public List<string> WithOrigins
        {
            get
            {
                var list = new List<string>();
                config.GetSection("CorsPolicyConfig:WithOrigins").Bind(list);
                return list;
            }
        }
        /*END - CORS Policy Setup*/
         
        public string EmailTemplatePath => config["FilePath:EmailTemplatePath"]; 
    }
}
