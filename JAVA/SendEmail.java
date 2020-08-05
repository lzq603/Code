import com.sun.mail.util.MailSSLSocketFactory;

import javax.activation.DataHandler;
import javax.activation.DataSource;
import javax.activation.FileDataSource;
import javax.mail.*;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeBodyPart;
import javax.mail.internet.MimeMessage;
import javax.mail.internet.MimeMultipart;
import java.security.GeneralSecurityException;
import java.util.Properties;

/*
 * 需要导入依赖：
<dependency>
	<groupId>com.sun.mail</groupId>
	<artifactId>javax.mail</artifactId>
	<version>1.6.2</version>
</dependency>
<dependency>
	<groupId>javax.activation</groupId>
	<artifactId>activation</artifactId>
	<version>1.1.1</version>
</dependency>
 */

public class Main {

    public static void main(String[] args) throws GeneralSecurityException {
        Main main = new Main();
        main.sendEmail("标题", "内容", "收件人");
    }

    public void sendEmail(String title, String content, String to) throws GeneralSecurityException {
        String from = "xxxxxx@qq.com";
        String host = "smtp.qq.com";
        Properties properties = System.getProperties();
        properties.setProperty("mail.smtp.host", host);
        properties.put("mail.smtp.auth", "true");
        MailSSLSocketFactory sf = new MailSSLSocketFactory();
        sf.setTrustAllHosts(true);
        properties.put("mail.smtp.ssl.enable", "true");
        properties.put("mail.smtp.ssl.socketFactory", sf);
        Session session = Session.getDefaultInstance(properties, new Authenticator() {
            @Override
            protected PasswordAuthentication getPasswordAuthentication() {
                return new PasswordAuthentication("xxxxxxx@qq.com", "*********");
            }
        });
        try {
            MimeMessage message = new MimeMessage(session);
            message.setFrom(new InternetAddress(from));
            message.addRecipients(Message.RecipientType.TO,to);
            message.setSubject(title);
            message.setContent(content,"text/html;charset=utf-8");
            Transport.send(message);
            System.out.println("邮件发送成功");
        }catch (MessagingException mex){
            mex.printStackTrace();
        }
    }
}
