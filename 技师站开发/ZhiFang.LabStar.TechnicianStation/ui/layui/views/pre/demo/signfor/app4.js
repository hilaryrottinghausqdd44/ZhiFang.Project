/**
 * @name: 模式4：样本签收
 * @author: liangyl
 * @version: 
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable:'views/pre/demo/modules/uxtable',
	basic: 'views/pre/demo/signfor/basic'
}).use(['basic','table','laytpl','uxutil'], function () {
	var $ = layui.$,
	    table=layui.table,
	    laytpl=layui.laytpl,
	    uxutil = layui.uxutil,
	    basic = layui.basic;
	 //获取医嘱信息列表服务
	var GET_SAMPLE_LIST_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/mode1.js';
	//获取签收信息服务
	var GET_SIGNFOR_LIST_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/signfor.js';
	//获取签收信息服务
	var GET_SIGNFOR_LIST_URL2 = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/signfor.js';
	//扫码服务
	var GET_SCAN_BARCODE_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/barcode.js';

	var where ={limit:10000,page:1};
    //医嘱信息列表加载
	basic.DrAdviceTable.onSearch(GET_SAMPLE_LIST_URL,where);
	//签收失败列表加载
    basic.FailTable.onSearch(GET_SIGNFOR_LIST_URL,where);
    //操作列表加载
    basic.RecordTable.onSearch(GET_SIGNFOR_LIST_URL2,where);
    //扫码,回车事件
	$('#txtBarCode').on('keydown', function (event) {
	    if (event.keyCode == 13) {
	    	basic.SearchBar.onScanBarCode(GET_SCAN_BARCODE_URL,where,$('#txtBarCode'));
	    }
    });
    //选择行操作
	table.on('row(LAY-sample-signfor)', function(obj) {
		basic.OtherMethod.linkSampleAndItem(obj.data);
        //签收失败列表加载
	    basic.FailTable.onSearch(GET_SIGNFOR_LIST_URL,where);
	    //操作列表加载
	    basic.RecordTable.onSearch(GET_SIGNFOR_LIST_URL2,where);
	});
});
