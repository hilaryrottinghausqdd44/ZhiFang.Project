/**
 * @name: 模式2：样本签收
 * @author: liangyl
 * @version: 
 */
layui.extend({
	uxutil: 'ux/util',
    uxtable:'views/pre/demo/modules/uxtable',
	basic: 'views/pre/demo/signfor/basic'
}).use(['basic','form','table','laytpl','laydate','uxutil'], function () {
	var $ = layui.$,
	    form=layui.form,
	    table=layui.table,
	    laytpl=layui.laytpl,
	    laydate = layui.laydate,
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

	//初始化时间
	basic.SearchBar.iniDate();
	form.render();
	//日期时间选择器
	    laydate.render({
	       elem: '#test-laydate-type-datetime-mode1'
	      ,type: 'datetime'
	    });
	    laydate.render({
	       elem: '#test-laydate-type-datetime-mode2'
	      ,type: 'datetime'
	    });
	var where ={limit:10000,page:1};
	//打包机列表创建
	basic.BalerTable.cretateTable();
	//打包机列表加载
	basic.BalerTable.onSearch(GET_SAMPLE_LIST_URL,where);
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

