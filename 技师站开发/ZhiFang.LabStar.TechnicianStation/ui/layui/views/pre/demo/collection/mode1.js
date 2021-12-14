/**
 * @name: 模式1-样本采集
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
	var GET_SCAN_BARCODE_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/barcode.js';
    //采样人查询
	var GET_SAMPLE_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/mode_not_CJ.js';
    
//  var columns = basic.CollectionTable.myTable.config.cols;
//  columns[0].splice(5,0,{field: '条码号',title: '条码号嗯嗯嗯嗯',width: 150,sort: true});
//  
    var where ={limit:10000,page:1};
    //样本采集加载
	basic.CollectionTable.onSearch(GET_LIST_URL,where);
	//扫码,回车事件
	$('#txtBarCode').on('keydown', function (event) {
	    if (event.keyCode == 13) {
	    	basic.searchtoolbar.onScanBarCode(GET_SCAN_BARCODE_URL,where,$('#txtBarCode'));
	    }
    });
    //扫码后调用服务，采集确认
    layui.onevent("barCode", "click", function(obj) {
// 	    layer.alert('扫码后调用服务，采集确认');
	});
});