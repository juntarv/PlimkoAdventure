using UnityEngine;
using UnityEngine.Networking;

public class ContactUs : MonoBehaviour
{
    // Email details
    public string recipientEmail = "reginaldquinn9@gmail.com";
    public string emailSubject = "Support Request";
    public string emailBody = "Please describe your issue or question.";

    // Method to be called when the "Contact Us" button is clicked
    public void OnContactUsButtonClicked()
    {
        // Create the email URL
        string emailUrl = CreateEmailUrl(recipientEmail, emailSubject, emailBody);

        // Open the default mail app with the email URL
        Application.OpenURL(emailUrl);
    }

    // Method to create the URL for the mailto link
    private string CreateEmailUrl(string email, string subject, string body)
    {
        // URL-encode the subject and body to ensure they are properly formatted
        subject = UnityWebRequest.EscapeURL(subject);
        body = UnityWebRequest.EscapeURL(body);

        // Construct the mailto URL
        return $"mailto:{email}?subject={subject}&body={body}";
    }
}
