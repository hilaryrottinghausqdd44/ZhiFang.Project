

$(function () {
    //年龄计算
    function countAge(birthday){
        if (birthday != null && birthday != "") {
            var timeStr = "";
            var age = 0;
            timeStr = birthday.replace(/-/g, "\/");
            //2016-03-01 00:00:00.0
            var index = timeStr.indexOf(".");
            if (index != -1) {
                timeStr = timeStr.substring(0, index);
            }
            var curDate = new Date();
            var oriDate = new Date(timeStr);

            var curYear = parseInt(curDate.getFullYear());//返回4位完整的年份
            var oriYear = parseInt(oriDate.getFullYear());

            var curMonth = parseInt(curDate.getMonth());//返回表示月份的数字，返回值是0（一月）到11（十二月），比当前月小1
            var oriMonth = parseInt(oriDate.getMonth());

            var curDay = parseInt(curDate.getDate());//返回月份的某一天，返回值是1~31之间的一个整数
            var oriDay = parseInt(oriDate.getDate());

            /*var curHours = parseInt(curDate.getHours());
            var oriHours = parseInt(oriDate.getHours());
    
            var curMinutes = parseInt(curDate.getMinutes());
            var oriMinutes = parseInt(oriDate.getMinutes());
    
            var curSeconds = parseInt(curDate.getSeconds());
            var oriSeconds = parseInt(oriDate.getSeconds());*/
            age = curYear - oriYear;
            if (curMonth > oriMonth) {
                return age;
            } else {
                if (curMonth == oriMonth) {
                    if (curDay > oriDay) {
                        return age;
                    } else {
                        if (curDay == oriDay) {
                            return age;
                        } else {
                            return age - 1;
                        }
                    }
                } else {
                    return age - 1;
                }
            }
        }
    };
    //清空
    function reCadClear() {
        $("#form_CName").textbox('setValue', "");
        $("#form_Age").textbox('setValue', "");
        $("#form_PatNo").textbox('setValue', "");
        $("#form_PersonID").textbox('setValue', "");
        $("#form_WardNo").textbox('setValue', "");
    };
    //启用读卡器
    function M05_OpenDevice() {
        var CertCtl = document.getElementById("CertCtl");
        try {
            var result = CertCtl.connect();
            var resulto = $.parseJSON(result);
            if (resulto.resultFlag != 0) {
                alert(resulto.errorMsg);
            }            
        } catch (e) {
        }
    };
    //身份证识别
    function M05_ReadIDCardInfo() {
        reCadClear();
        var CertCtl = document.getElementById("CertCtl");

        try {
            var startDt = new Date();
            var result = CertCtl.readCert();
            var endDt = new Date();

            //document.getElementById("timeElapsed").value = (endDt.getTime() - startDt.getTime()) + "毫秒";
            //document.getElementById("result").value = result;

            var resultObj = $.parseJSON(result);
            if (resultObj.resultFlag == 0) {

                $("#form_CName").textbox('setValue', resultObj.resultContent.partyName);
                if (resultObj.resultContent.gender != null) {
                    if (resultObj.resultContent.gender == '男') {
                        $("#form_GenderNo").combobox("setValue", 1);//性别-值
                    } else if (resultObj.resultContent.gender == '女') {
                        $("#form_GenderNo").combobox("setValue", 2);//性别-值
                    } else {
                        $("#form_GenderNo").combobox("setValue", 3);//性别-值
                    }
                } else {
                    $("#form_GenderNo").combobox("setValue", resultObj.resultContent.gender || "");//性别-值
                }
                resultObj.resultContent.bornDay;


                $("#form_PersonID").textbox('setValue', resultObj.resultContent.certNumber);
                $("#form_WardNo").textbox('setValue', resultObj.resultContent.certAddress);
                var year = resultObj.resultContent.bornDay.substring(0, 4);
                var mth = resultObj.resultContent.bornDay.substring(4, 6);
                var day = resultObj.resultContent.bornDay.substring(6, 8);
                var bih = year + "-" + mth + "-" + day;
                $("#form_Age").textbox('setValue', countAge(bih));




                //document.getElementById("partyName").value = resultObj.resultContent.partyName;
                //document.getElementById("gender").value = resultObj.resultContent.gender;
                //document.getElementById("nation").value = resultObj.resultContent.nation;
                //document.getElementById("bornDay").value = resultObj.resultContent.bornDay;
                //document.getElementById("certAddress").value = resultObj.resultContent.certAddress;
                //document.getElementById("certNumber").value = resultObj.resultContent.certNumber;
                //document.getElementById("certOrg").value = resultObj.resultContent.certOrg;
                //document.getElementById("effDate").value = resultObj.resultContent.effDate;
                //document.getElementById("expDate").value = resultObj.resultContent.expDate;
                //document.getElementById("certType").value = resultObj.resultContent.certType;
                //document.getElementById("signNum").value = resultObj.resultContent.signNum;
                //document.getElementById("passportNo").value = resultObj.resultContent.passportNo;
                //document.getElementById("chineseName").value = resultObj.resultContent.chineseName;
                //document.getElementById("PhotoStr").src = "data:image/jpeg;base64," + resultObj.resultContent.identityPic;
            }
            else {
                alert("读卡失败错误代码:" + resultObj.resultFlag + ",错误信息:" + resultObj.errorMsg);
            }
        } catch (e) {
            alert(e);
        }
    };

    //加载启动
    $("#IdCard").click(M05_ReadIDCardInfo);
    //$("#Insurance").click(M05_ReadSSSECardInfo);
    M05_OpenDevice();
});