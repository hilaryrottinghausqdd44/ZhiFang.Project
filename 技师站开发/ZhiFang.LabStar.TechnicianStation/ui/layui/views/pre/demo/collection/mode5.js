/**
 * @name: 模式5-样本采集
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
}).use(['basic','uxutil','table','form'], function () {
	var $ = layui.$,
	    form = layui.form,
	    table =layui.table, 
		uxutil=layui.uxutil,
	    basic = layui.basic;
	//获取样本采集列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/mode1.js';
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
	//样未采集加载
	basic.UnCollectionTable.cretateTable();	
	//样未采集加载
	basic.UnCollectionTable.onSearch(GET_UNLIST_URL,where);
	//扫码,回车事件
	$('#txtBarCode').on('keydown', function (event) {
	    if (event.keyCode == 13) {
	    	basic.searchtoolbar.onScanBarCode(GET_SCAN_BARCODE_URL,where,$('#txtBarCode'));
	    }
    });
    //样本未采集列表，数据返回
    layui.onevent("uncollection", "done", function(params) {
    	var res =params.res,
	   	    curr =params.curr,
	   	    that=params.that,
	   	    count =params.count;
	    var collectiondata =table.cache["LAY-collection-mode"];
		res.data.forEach(function (item, index) {
		 	if (item.急查 === "急" ){
		 		var tr = that.find(".layui-table-box tbody tr[data-index='" + index + "']").css("background-color", "#FF4500");
		 	}
            for(var i=0;i<collectiondata.length;i++){
            	var barcode = collectiondata[i].条码号;
            	if(barcode==item.条码号){
            		that.find(".layui-table-box tbody tr[data-index='" + index + "']").find('td:eq(2)').css("background-color", "#00EE76");
            		continue;
            	}
            }
		});
	});
	//查询采样确认监听
	form.on('submit(LAY-mode-confirm-from-search)', function(data) {
		var field = data.field;
		var strWhere = '';
//		var obj ={
//			
//		}
	});
});