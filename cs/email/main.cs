using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;


public class Program
{
	const bool DEBUG  = false;
	const string PROGRAM_NAME = "sender";
	private static readonly int EXPECTED_ARGS = 4;
	public static void Main(string[] args)
	{
		if (args.Length > 0)
		{
			if (args[0] != "-help")
			{
				string signature, subject, body, error;
				string[] attachments, addresses;
				if (parse_args(args, out addresses, out signature, out subject, out body, out attachments, out error))
				{
					Console.WriteLine("Sending...");
					if (DEBUG)
					{
						foreach (string address in addresses) Console.WriteLine(address);
						Console.WriteLine(signature);
						Console.WriteLine(subject);
						Console.WriteLine(body);
						foreach (string i in attachments) Console.WriteLine(i);
					}
					else
					{
						SendEmail(addresses, signature, subject, body, attachments);
						Console.WriteLine("Sent.");
					}
				}
				else
				{
					Console.WriteLine("Error: " + error);
					ShowHelp();
				}
			}
			else
			{
				Console.WriteLine("Usage: sender [ADDRESS1:ADDRESS2:...] [SIGNATURE] [SUBJECT] [BODY] {-attach} {file1} {file2...}");
				Console.WriteLine("[ADDRESS1:ADDRESS2:...]:   The target email addresses, separated by a colon, e.g. \"someone@example.com:someone2@example2.com\".");
				Console.WriteLine("[SIGNATURE]:               The signature of the sender, to be displayed as a sender name, e.g. \"Automated Messenger\".");
				Console.WriteLine("[SUBJECT]:                 The subject of the email, e.g. \"An Automated Message\".");
				Console.WriteLine("[BODY]:                    The body of the email, e.g. \"Hello World!\".");
				Console.WriteLine("{-attach} (optional):      Option to specify attachments.");
				Console.WriteLine("{file1} (optional):        A file to attach.");
			}
		}
		else
		{
			Console.WriteLine("Found 0 arguments, expecting 4 or more.");
			ShowHelp();
		}
	}
	public static bool parse_args(string[] args, out string[] target_addresses, out string signature, out string subject, out string body_contents, out string[] attachments, out string error)
	{
		target_addresses = new string[] {}; signature = ""; subject = ""; body_contents = ""; error = "NONE"; attachments = new string[]{};
		if (args.Length < EXPECTED_ARGS) {error = "not enough arguments (" + args.Length + ")."; return false;}
		target_addresses = args[0].Split(':');
		foreach (string target_address in target_addresses)
		{
			if(!(target_address.Contains("@") && target_address.Contains("."))) {error = "could not detect valid email address for argument " + target_address + "."; return false;}
		}
		signature = args[1];
		subject = args[2];
		body_contents = args[3];
		List<string> attch_list = new List<string>();
		if (args.Length > EXPECTED_ARGS)
		{
			if (args[EXPECTED_ARGS] == "-attach")
			{
				if (args.Length > EXPECTED_ARGS + 1)
				{
					for (int i = EXPECTED_ARGS + 1; i < args.Length; i++)
					{
						if (File.Exists(args[i]))
						{
							attch_list.Add(args[i]);
						}
						else
						{
						error = "could not find file " + args[i] + ".";
						return false;
						}
					}
				}
				else
				{
					error = "no attachments passed.";
					return false;
				}
			}
			else
			{
				error = "unkown option " + args[EXPECTED_ARGS] + ".";
				return false;
			}
		}
		attachments = attch_list.ToArray();
		return true;
	}
	public static void ShowHelp()
	{
		Console.WriteLine("Try \"" + PROGRAM_NAME + " -help\".");
	}
	public static void SendEmail(string[] addresses, string signature, string subject, string body, string[] file_attachments)
	{
		if (addresses.Length > 0)
		{
			//THIS WORKS!!!!!
			var fromAddress = new MailAddress("uk.jenkins.two@gmail.com", signature);
			var toAddress = new MailAddress(addresses[0]);
			const string p = "cfd-is-for-winners";

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, p)
			};
			using (var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
				Body = body
			})
			{
				for (int i = 1; i < addresses.Length; i++) message.To.Add(addresses[i]);
				foreach (string file in file_attachments)
				{
					System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(file);
					message.Attachments.Add(attachment);
				}
				smtp.Send(message);
			}
		}
		else
		{
			Console.WriteLine("Error: no target addresses specified.");
		}
	}
	public static void SendEmail(string[] addresses, string subject, string body)
	{
		SendEmail(addresses, "AUTOMATIC EMAIL", subject, body, new string[]{});
	}
}
