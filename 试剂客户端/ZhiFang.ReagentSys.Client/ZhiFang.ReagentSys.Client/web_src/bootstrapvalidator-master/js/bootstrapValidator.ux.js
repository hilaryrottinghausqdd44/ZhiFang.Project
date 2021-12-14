/*
 * 因bootstrapValidator不支持中国身份证格式验证，所以看了源码的校验机制。
 * 采用扩展追加方式，使之支持中国身份证格式校验。
 * 使用时，在加载完bootstrapValidator.js后再加载本JS文件。
 * @version 2017-05-03 21:02
 * @author Jcall
 */
(function($) {
	$.fn.bootstrapValidator.i18n.id.countries.CN = '中国';
	$.fn.bootstrapValidator.validators.id.COUNTRY_CODES.push('CN');
	$.fn.bootstrapValidator.validators.id._cn = function(value){
		var filter = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
        return filter.test(value);
	}
}(window.jQuery));