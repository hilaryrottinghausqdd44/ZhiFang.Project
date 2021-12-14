/**
 * @name: 模式4-样本采集
 * @author: liangyl
 * @version: 
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable:'views/pre/demo/modules/uxtable',
    common:'views/pre/demo/modules/common',
	uxform:'views/pre/demo/modules/uxform',
	uxcombobox:'views/pre/demo/modules/uxcombobox',
	basic: 'views/pre/demo/collection/basic'
}).use(['basic','uxutil'], function () {
	var $ = layui.$,
		uxutil=layui.uxutil,
	    basic = layui.basic;
	//获取样本采集列表服务
	var GET_LIST_URL =  uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/mode1.js';
	//获取未采集服务
	var GET_UNLIST_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/mode_not_CJ.js';
    //扫码服务
	var GET_SCAN_BARCODE_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/mode_not_CJ.js';

    //初始化时间
    basic.searchtoolbar.iniDate();
    //查询工具栏收缩-展开
    basic.searchtoolbar.isShow();
    var where ='';
    //样本采集加载
	basic.CollectionTable.onSearch(GET_LIST_URL,where); 

	//扫码,回车事件
	$('#txtBarCode').on('keydown', function (event) {
	    if (event.keyCode == 13) {
	    	basic.searchtoolbar.onScanBarCode(GET_SCAN_BARCODE_URL,where,$('#txtBarCode'));
	    }
    });
});