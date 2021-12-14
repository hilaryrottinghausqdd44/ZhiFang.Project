$(function () {
    $('#BarcodeTxt').textbox({
        prompt: '预置条码(必填)',
        required: true
    });
    $('#NameTxt').textbox({
        prompt: '姓名(必填)',
        required: true
    });
//    $('#CheckTxt').textbox({
//        prompt: '验证码(必填)'//,
////        onChange: function () {
////            $.get("ServiceWCF/ReportFromService.svc/PersonSearch_ValidateCode_PKI?tmpvalidatecode=" + $('#CheckTxt').val(), function (data, status) {
////                //alert("Data: " + data + "\nStatus: " + status);
////            });
////        }
//    });

    $.extend($.fn.validatebox.defaults.rules, {
        minLength: {
            validator: function (value, param) {
                return value.length >= param[0];
            },
            message: 'Please enter at least {0} characters.'
        }
    });
});
function getCookie(name) {
    var nameT = Shell.util.Cookie.mapping[name] || name,
			reg = new RegExp("(^| )" + nameT + "=([^;]*)(;|$)"),
			arr = document.cookie.match(reg);

    if (arr) return unescape(decodeURI(arr[2]).replace(/\+/g, "%20"));
    return null;
}
/**设置cookie属性*/
function setCookie(name, value) {

    document.cookie = name + "=" + escape(value);
}
function RefreshCheckImg() {
    $("#ImageCheck").attr("src", "Util/ValidateCode.aspx?flag=" + Math.random());
}
function ValidateForm() {
    if ($('#BarcodeTxt').val() != "") {
        setCookie('BarcodeTxt', $('#BarcodeTxt').val());
        //Shell.util.Cookie.setCookie("BarcodeTxt", $('#BarcodeTxt').val());
        if ($('#NameTxt').val() != "") {
            setCookie('NameTxt', $('#NameTxt').val());
            //Shell.util.Cookie.setCookie("NameTxt", $('#NameTxt').val());
            setCookie('ZhiFangUser', 'tmpuser');
            if ($('#CheckTxt').val() != "") {
                $.ajax({
                    url: "ServiceWCF/ReportFromService.svc/PersonSearch_ValidateCode_PKI?tmpvalidatecode=" + $('#CheckTxt').val(),
                    async: false,
                    success: function (data) {
                        if (data && data == true) {
                            $('#ErrorInfo').text('');
                            //alert('验证码正确！');
                            return true;
                        }
                        else {
                            $('#ErrorInfo').text('验证码错误！');
                            //alert('验证码错误！');
                            $('#CheckTxt').textbox('textbox').focus();
                            return false;
                        }
                    }
                });

//                $.get("ServiceWCF/ReportFromService.svc/PersonSearch_ValidateCode_PKI?tmpvalidatecode=" + $('#CheckTxt').val(), function (data, status) {
//                    if (data && data == true) {
//                        return true;
//                    }
//                    else {
//                        alert('验证码错误！');
//                        $('#CheckTxt').textbox('textbox').focus();
//                        return false;
//                    }
                //                });
                //return false;
            }
            else {
                $('#ErrorInfo').text('请输入验证码！');
                $('#CheckTxt').textbox('textbox').focus();
                return false;
            }
        }
        else {
            $('#NameTxt').textbox('textbox').focus();
            return false;
        }
    }
    else {
        $('#BarcodeTxt').textbox('textbox').focus();
        return false;
    }
}