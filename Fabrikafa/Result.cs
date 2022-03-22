using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrikafa;

[Serializable]
public class Result
{
    /// <summary>
    /// Bolean result of the operation.
    /// </summary>
    public bool Value { get; set; }
    
    /// <summary>
    /// Code represents the operation result.
    /// </summary>
    public ResultCodeEnum Code { get; set; }

    /// <summary>
    /// Message type for how the operation result is treated.
    /// </summary>
    public MessageTypeEnum MessageType { get; set; }

    /// <summary>
    /// Title to summarize result of the operation. If ommited MessageType is used to populate.
    /// This is usually more generic and broad than the FriendlyDescription value.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// A more detailed message for users. If omitted template is used to populate.
    /// </summary>
    public string FriendlyDescription { get; set; }

    /// <summary>
    /// Url value to redirect when user clicks to main (CTA) button.
    /// </summary>
    public string CallToActionUrl { get; set; }

    /// <summary>
    /// Displayed text for the main (CTA) Button
    /// </summary>
    public string CallToActionUrlText { get; set; }
    
    /// <summary>
    /// Url value to redirect when user clicks to cancel.
    /// </summary>
    public string CallBackUrl { get; set; }

    /// <summary>
    /// Displayed text for the Call Back Button
    /// </summary>
    public string CallBackUrlText { get; set; }

    /// <summary>
    /// messageText value to use with template
    /// </summary>
    private static string messageText = string.Empty;

    /// <summary>
    /// Text representation of the current result code enum value to use with template
    /// </summary>
    private static string codeText = string.Empty;

    /// <summary>
    /// Template for combining messageText and codeText: "{messageText} ({codeText})"
    /// </summary>
    private string template = $"{messageText} ({codeText})";
    
    // <summary>
    /// Default constructor creates a default general error result
    /// </summary>
    public Result()
    {
        this.Value = false;
        this.Code = ResultCodeEnum.GeneralError;
        this.MessageType = MessageTypeEnum.error;
        this.Title = MessageType.ToString();
        messageText = "General error occured";
        codeText = ((int)this.Code).ToString();
        this.FriendlyDescription = template;
        CallToActionUrl = string.Empty;
        CallToActionUrlText = string.Empty;
        CallBackUrl = "/";
        CallBackUrlText = "Go to Home";
    }

    /// <summary>
    /// Create a result instance with the given parameters.
    /// If given result code is undefined in the ResultCodeEnum given message prefixed with "General error occured:"
    /// </summary>
    /// <param name="ResultCode">Numeric result code.</param>
    /// <param name="ResultValue">Bolean result of the operation.</param>
    /// <param name="Message">Message to summarize operation result. This is usually more specific and explainatary than the Title value.</param>
    /// <param name="MessageType">Message type for how the operation result is treated.</param>
    public Result(int ResultCode, bool ResultValue, string Message, MessageTypeEnum MessageType = MessageTypeEnum.warning)
    {
        string resultCodeText = ResultCode.ToString();
        ResultCodeEnum enumValue = (ResultCodeEnum)Enum.Parse(typeof(ResultCodeEnum), resultCodeText);

        if (!Enum.IsDefined(typeof(ResultCodeEnum), enumValue) && !enumValue.ToString().Contains(","))
        {
            messageText = $"General error occured: {Message}";
        }
        else
        {
            messageText = Message;
        }

        codeText = resultCodeText;

        this.FriendlyDescription = template;
        this.Code = enumValue;
        this.Value = ResultValue;
        this.MessageType = MessageType;
        this.Title = MessageType.ToString();
    }
}

/// <summary>
/// Pre-defined message type
/// </summary>
public enum MessageTypeEnum
{
    primary,
    info,
    warning,
    success,
    error
}

/// <summary>
/// Pre-defined result codes
/// </summary>
public enum ResultCodeEnum
{
    Success = 0,
    GeneralError = -1,
    AccountActivationError = -100,
    AccountAccessError = -110,
    RecordExists = -1000
}
