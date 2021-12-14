/**统一放置于JcallShell中*/
var JcallShell = JcallShell || {};

/**校验处理*/
JcallShell.Isvalid = {
    /**
	 * 手机号码
	 * 移动：134[0-8],135,136,137,138,139,150,151,157,158,159,182,187,188
	 * 联通：130,131,132,152,155,156,185,186
	 * 电信：133,1349,153,180,189
	 */
    isCellPhoneNo: function (value) {
        if (!value || value.length != 11) return false;
        //var filter = /^1[1-9][0-9]\d{4,8}$/;
        //return filter.test(value);
        return true;
    },
    /**
	 * 身份证号码
	 * 支持15位和18位身份证号码校验
	 */
    isIdCardNo: function (value) {
        var filter = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
        return filter.test(value);
    }
};