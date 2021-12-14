using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region FFeedback

    /// <summary>
    /// FFeedback object for NHibernate mapped table 'F_Feedback'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "意见反馈表", ClassCName = "FFeedback", ShortCode = "FFeedback", Desc = "意见反馈表")]
    public class FFeedback : BaseEntity
    {
        #region Member Variables

        protected DateTime? _feedbackTime;
        protected string _feedbackContent;
        protected DateTime? _answerTime;
        protected string _answerContent;
        protected bool _isUse;
        protected string _memo;
        protected string _phone;
        protected string _email;
        protected DateTime? _dataUpdateTime;
        protected HREmployee _answer;
        protected HREmployee _creator;

        #endregion

        #region Constructors

        public FFeedback() { }

        public FFeedback(DateTime feedbackTime, string feedbackContent, DateTime answerTime, string answerContent, bool isUse, string memo, string phone, string email, DateTime dataUpdateTime, DateTime dataAddTime, HREmployee answer, HREmployee creator)
        {
            this._feedbackTime = feedbackTime;
            this._feedbackContent = feedbackContent;
            this._answerTime = answerTime;
            this._answerContent = answerContent;
            this._isUse = isUse;
            this._memo = memo;
            this._phone = phone;
            this._email = email;
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
            this._answer = answer;
            this._creator = creator;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "反馈时间", ShortCode = "FeedbackTime", Desc = "反馈时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FeedbackTime
        {
            get { return _feedbackTime; }
            set { _feedbackTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "反馈内容", ShortCode = "FeedbackContent", Desc = "反馈内容", ContextType = SysDic.All, Length = 500)]
        public virtual string FeedbackContent
        {
            get { return _feedbackContent; }
            set { _feedbackContent = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "回复时间", ShortCode = "AnswerTime", Desc = "回复时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AnswerTime
        {
            get { return _answerTime; }
            set { _answerTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "回复内容", ShortCode = "AnswerContent", Desc = "回复内容", ContextType = SysDic.All, Length = 500)]
        public virtual string AnswerContent
        {
            get { return _answerContent; }
            set { _answerContent = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        [DataMember]
        [DataDesc(CName = "手机号", ShortCode = "Phone", Desc = "手机号", ContextType = SysDic.All, Length = 50)]
        public virtual string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        [DataMember]
        [DataDesc(CName = "邮箱地址", ShortCode = "Email", Desc = "邮箱地址", ContextType = SysDic.All, Length = 100)]
        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "修改时间", ShortCode = "DataUpdateTime", Desc = "修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "回复者", ShortCode = "Answer", Desc = "回复者")]
        public virtual HREmployee Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        [DataMember]
        [DataDesc(CName = "反馈者", ShortCode = "Creator", Desc = "反馈者")]
        public virtual HREmployee Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }


        #endregion
    }
    #endregion
}