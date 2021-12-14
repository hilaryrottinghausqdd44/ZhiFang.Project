

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
        var ret = M05RdCard.OpenDevice();
        if (ret > 0) {
            handle = ret;
            document.getElementById("ResultDisplay").value = ret;
        }
        else {
            //M05_ErrorDetect(ret);
            document.getElementById("ResultDisplay").value = ret;
        }
    };
    //身份证识别
    function M05_ReadIDCardInfo() {
        reCadClear();
        var ret = M05RdCard.ReadIDCardInfo();
        if (ret == 0) {
            $("#form_CName").textbox('setValue', M05RdCard.IDName);

            if (M05RdCard.IDSex != null) {
                if (M05RdCard.IDSex == '男') {
                    $("#form_GenderNo").combobox("setValue", 1);//性别-值
                } else if (M05RdCard.IDSex == '女') {
                    $("#form_GenderNo").combobox("setValue", 2);//性别-值
                } else {
                    $("#form_GenderNo").combobox("setValue", 3);//性别-值
                }
            } else {
                $("#form_GenderNo").combobox("setValue", M05RdCard.IDSex || "");//性别-值
            }

            //$("#form_FolkNo").combobox({
            //    onLoadSuccess: function () { //数据加载完毕事件
            //        var data = $("#form_FolkNo").combobox('getData');
            //        var defaultTeam = M05RdCard.IDFolk;
            //        if (data.length > 0) {
            //            if (defaultTeam != null && defaultTeam != "") {
            //                for (var i = 0; i < data.length; i++) {
            //                    if (data[i].CName.indexOf(defaultTeam)>=0) {
            //                        $("#form_FolkNo").combobox('select', data[i].CName);
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //});

            $("#form_PersonID").textbox('setValue', M05RdCard.IDNumber);
            $("#form_WardNo").textbox('setValue', M05RdCard.IDAddress);
            var year = M05RdCard.IDBirth.substring(0, 4);
            var mth= M05RdCard.IDBirth.substring(4, 6);
            var day = M05RdCard.IDBirth.substring(6, 8);
            var bih = year + "-" + mth + "-" + day;
            $("#form_Age").textbox('setValue', countAge(bih));
            //document.getElementById("IDFolk").value = M05RdCard.IDFolk;
            //document.getElementById("IDBirth").value = M05RdCard.IDBirth;
            //document.getElementById("IDNumber").value = M05RdCard.IDNumber;
            //document.getElementById("IDAddress").value = M05RdCard.IDAddress;
            //document.getElementById("IDOrgan").value = M05RdCard.IDOrgan;
            //document.getElementById("IDTermBegin").value = M05RdCard.IDTermBegin;
            //document.getElementById("IDTermEnd").value = M05RdCard.IDTermEnd;
            //document.getElementById("IDNewAddress").value = M05RdCard.IDNewAddress;
            //document.getElementById("IDPhotoPath").value = M05RdCard.IDPhotoPath;
            //var pathtmp = M05RdCard.IDPhotoPath + M05RdCard.IDNumber + ".jpg";
            //document.getElementById("IDPhoto").src = pathtmp;

            //document.getElementById("ResultDisplay").value = ret;
        }
        else if (handle <= 0) {
            //M05_ErrorDetect(ret);
            document.getElementById("ResultDisplay").value = ret;
        }
        else {
            //M05_ErrorDetect(ret);
            document.getElementById("ResultDisplay").value = ret;
        }
    };
    //社保卡
    function M05_ReadSSSECardInfo() {
        reCadClear();
        var ret = M05RdCard.ReadSSSECardInfo();
        if (ret == 0) {
            $("#form_CName").textbox('setValue', M05RdCard.SSSEName);
            if (M05RdCard.SSSESex != null) {
                if (M05RdCard.SSSESex == '男') {
                    $("#form_GenderNo").combobox("setValue", 1);//性别-值
                } else if (M05RdCard.SSSESex == '女') {
                    $("#form_GenderNo").combobox("setValue", 2);//性别-值
                } else {
                    $("#form_GenderNo").combobox("setValue", 3);//性别-值
                }
            } else {
                $("#form_GenderNo").combobox("setValue", M05RdCard.SSSESex || "");//性别-值
            }
           
            $("#form_PersonID").textbox('setValue', M05RdCard.SSSEIDNumber);
            var year = M05RdCard.SSSEBirth.substring(0, 4);
            var mth = M05RdCard.SSSEBirth.substring(4, 6);
            var day = M05RdCard.SSSEBirth.substring(6, 8);
            var bih = year + "-" + mth + "-" + day;
            //民族：里面是编号不知道为什么    M05RdCard.SSSENation
            $("#form_Age").textbox('setValue', countAge(bih));
            $("#form_PatNo").textbox('setValue', M05RdCard.SSSECardID);
        }
        else if (handle <= 0) {
            //M05_ErrorDetect(ret);
            document.getElementById("ResultDisplay").value = ret;
        }
        else {
            //M05_ErrorDetect(ret);
            document.getElementById("ResultDisplay").value = ret;
        }
    };


    //加载启动
    $("#IdCard").click(M05_ReadIDCardInfo);
    $("#Insurance").click(M05_ReadSSSECardInfo);
    M05_OpenDevice();
});