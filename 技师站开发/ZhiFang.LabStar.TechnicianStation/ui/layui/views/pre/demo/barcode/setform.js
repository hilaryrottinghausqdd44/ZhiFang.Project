/**
 @Name：配置界面
 @Author：
 @version 2016-12-27
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['form'],function(){
	var $ = layui.$,
		form = layui.form;
	form.render();
	if(localStorageget('printConfig')){
		form.val('LAY-setform-form',JSON.parse(localStorageget('printConfig')));
	}
    /**本地数据存储*/
    function localStorageset(name, value) {
		localStorage.setItem(name, value);
	}
	function localStorageget(name) {
		return localStorage.getItem(name);
	}
	function localStorageremove(name) {
		localStorage.removeItem(name);
	}
});
