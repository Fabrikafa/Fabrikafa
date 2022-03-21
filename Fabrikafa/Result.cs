using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrikafa;

[Serializable]
public class Result
{
    public bool Value { get; set; }
    public ResultCodeEnum Code { get; set; }
    public MessageTypeEnum MessageType { get; set; }
    public string Title { get; set; }
    public string FriendlyDescription { get; set; }
    public string CallToActionUrl { get; set; }
    public string CallToActionUrlText { get; set; }
    public string CallBackUrl { get; set; }
    public string CallBackUrlText { get; set; }

    static string messageText = string.Empty;
    static string codeText = string.Empty;
    string template = $"{messageText} ({codeText})";

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

    public Result(int ResultCode, bool ResultValue, string Message, MessageTypeEnum MessageType = MessageTypeEnum.warning)
    {
        string resultCodeText = ResultCode.ToString();
        ResultCodeEnum enumValue = (ResultCodeEnum)Enum.Parse(typeof(ResultCodeEnum), resultCodeText);

        if (!Enum.IsDefined(typeof(ResultCodeEnum), enumValue) && !enumValue.ToString().Contains(","))
        {
            messageText = "General error occured";
            codeText = resultCodeText;
            this.FriendlyDescription = template;
        }
        else
        {
            messageText = Message;
            codeText = resultCodeText;
            this.FriendlyDescription = template;
        }

        this.Code = enumValue;
        this.Value = ResultValue;
        this.MessageType = MessageType;
        this.Title = MessageType.ToString();
    }
}

public enum MessageTypeEnum
{
    primary,
    info,
    warning,
    success,
    error
}

public enum ResultCodeEnum
{
    Success = 0,
    GeneralError = -1,
    AccountActivationError = -100,
    AccountAccessError = -110,
    RecordExists = -1000
}
