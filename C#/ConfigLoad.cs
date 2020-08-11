using System.Collections.Generic;
using System.Text;
using System.IO;

/*
 *使用示例
	//读取配置
	ConfigLoad con = new ConfigLoad("config.ini");
	//textbox读取配置文件默认值
	textBox1.Text = con.GetStringValue("url1");
	textBox2.Text = con.GetStringValue("url2");
	
	//写入配置
	ConfigSet cs = new ConfigSet("config.ini", true);
	cs.SetConfigValue("url1", textBox1.Text.ToString());
	cs.WriteConfigToFile(ConfigFile.newFile);
 */
	
namespace HisInterfaceValidate
{
    class ConfigLoad
    {
        public ConfigLoad(string conFilePath)
        {
            this.conFilePath = conFilePath;
            ReadConfig();


        }
        /// <summary>
        /// 配置文件的目录
        /// </summary>
        #region ConFilePath
        string conFilePath;
        public string ConFilePath
        {
            set { conFilePath = value; }
            get { return conFilePath; }
        }
        #endregion


        /// <summary>
                /// 配置文件属性值
                /// </summary>
        private List<string> configName = new List<string>();//名称集合
        private List<string> configValue = new List<string>(); //数值集合


        /// <summary>
                /// 读取配置文件的属性值
                /// </summary>
        public bool ReadConfig()
        {
            //检查配置文件是否存在
            if (!File.Exists(this.conFilePath))
            {
                return false;
            }


            StreamReader sr = new StreamReader(this.conFilePath, Encoding.UTF8);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                string cName, cValue;
                string[] cLine = line.Split('=');
                if (cLine.Length == 2)
                {
                    cName = cLine[0];
                    cValue = cLine[1];
                    configName.Add(cName);
                    configValue.Add(cValue);
                }


            }
            sr.Close();
            return true;


        }
        #region GetConfigValue
        /// <summary>
                /// 返回变量的字符串值
                /// </summary>
                /// <param name="cName">变量名称</param>
                /// <returns>变量值</returns>


        public string GetStringValue(string cName)
        {


            for (int i = 0; i < configName.Count; i++)
            {
                if (configName[i].Equals(cName))
                {
                    return configValue[i];


                }
            }


            return null;
        }
        public int GetIntValue(string cName)
        {


            for (int i = 0; i < configName.Count; i++)
            {
                if (configName[i].Equals(cName))
                {
                    int result;
                    if (int.TryParse(configValue[i], out result))
                    {
                        return result;
                    }


                }
            }


            return 0;
        }
        public float GetFloatValue(string cName)
        {


            for (int i = 0; i < configName.Count; i++)
            {
                if (configName[i].Equals(cName))
                {
                    float result;
                    if (float.TryParse(configValue[i], out result))
                    {
                        return result;
                    }


                }
            }


            return 0;
        }
        #endregion


    }


    public class ConfigSet
    {
        public ConfigSet(string configFilePath, bool isRead)
        {
            this.configFilePath = configFilePath;
            if (File.Exists(this.configFilePath) && isRead)
            {
                ReadConfig();
            }
        }
        public ConfigSet(string configFilePath)
        {
            this.configFilePath = configFilePath;


        }
        private string configFilePath;
        private List<string> configName = new List<string>();//名称集合
        private List<string> configValue = new List<string>(); //数值集合


        /// <summary>
                /// 设置写入配置文件的值
                /// </summary>
                /// <param name="cName">属性名称</param>
                /// <param name="cValue">值</param>
        public void SetConfigValue(string cName, string cValue)
        {
            bool ishere = false;


            //检查是否已经存在.
            if (configName.Count != 0)
            {
                for (int i = 0; i < configName.Count; i++)
                {
                    if (configName[i].Equals(cName))
                    {
                        configValue[i] = cValue;
                        ishere = true;
                    }
                }


            }
            if (!ishere)
            {
                configName.Add(cName);
                configValue.Add(cValue);
            }




        }
        /// <summary>
        /// 将设置的值写入到ini文件中.
        /// </summary>
        /// <param name="cf">是否追加到文件</param>
        /// <returns></returns>
        public bool WriteConfigToFile(ConfigFile cf)
        {
            StreamWriter sw;


            switch (cf)
            {
                case ConfigFile.newFile:
                    {


                        sw = new StreamWriter(this.configFilePath, false);


                        break;
                    };
                case ConfigFile.appendFile:
                    {
                        sw = new StreamWriter(this.configFilePath, true);
                        break;
                    }
                default:
                    {
                        sw = new StreamWriter(this.configFilePath);
                        break;
                    }
            }
            try
            {
                for (int i = 0; i < configName.Count; i++)
                {
                    sw.WriteLine("{0}={1}", configName[i], configValue[i]);
                }
            }


            catch
            {
                return false;


            }
            finally
            {
                sw.Close();
            }
            return true;


        }


        /// <summary>
                /// 读取配置文件的属性值
                /// </summary>
        private bool ReadConfig()
        {


            StreamReader sr = new StreamReader(this.configFilePath, Encoding.UTF8);
            string line;
            while ((line = sr.ReadLine()) != null)
            {




                line = line.Trim();
                string cName, cValue;
                string[] cLine = line.Split('=');
                if (cLine.Length == 2)
                {
                    cName = cLine[0];
                    cValue = cLine[1];
                    configName.Add(cName);
                    configValue.Add(cValue);
                }


            }
            sr.Close();
            return true;


        }
    }
    /// <summary>
    /// 写入文件属性
    /// </summary>
    public enum ConfigFile { newFile, appendFile }
}