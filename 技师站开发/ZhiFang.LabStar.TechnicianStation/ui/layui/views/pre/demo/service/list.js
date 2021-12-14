/**
 * @name: 样本送达， mode=1 样本送达模式1 ； mode=2 样本送达模式2
 * @author: liangyl
 * @version: 
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable:'views/pre/demo/modules/uxtable',
	basic: 'views/pre/demo/service/basic'
}).use(['table','basic','uxutil'], function () {
	var $ = layui.$,
	    basic =layui.basic,
		uxutil=layui.uxutil,
		table = layui.table;
	//当前模式
    var Mode=parMode();
    var GET_LIST_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/mode1.js';
    //扫码服务
	var GET_SCAN_BARCODE_URL = uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/barcode.js';

	//创建列表
	basic.ServiceTable.cretateTable(Mode);
	var where ={limit:10000,page:1};
    //样本送达列表加载
	basic.ServiceTable.onSearch(GET_LIST_URL,where);
    //扫码,回车事件
	$('#txtBarCode').on('keydown', function (event) {
	    if (event.keyCode == 13) {
	    	
	    	//模式1  验证护工信息送达
    		if(Mode=='1'){
    			if(!$('#txtTransport').val()){
		    		layer.msg('运送人不能为空',{icon:2,time:1000});
		    		return;
    			}
    			//调用服务验证运送人
    			
    		}
	    	basic.SearchBar.onScanBarCode(GET_SCAN_BARCODE_URL,where,$('#txtBarCode'),Mode);
	    }
    });
     //扫码后调用服务
    layui.onevent("barCode", "click", function(obj) {
// 	    layer.alert('扫码后调用服务，保存');
	});
    function parMode (){
    	var Mode ='2';
    	var params = uxutil.params.get(true);
	    if(params.MODE && params.MODE=='1'){//模式1
	    	$('#TransportCel').show();
	    	Mode='1';
	    }else{//模式2
	    	$('#TransportCel').hide();
	    	Mode='2';
	    }
	    return Mode;
    }
    //头部工具栏事件
    table.on('toolbar(LAY-sample-service)', function(obj){
	    var checkStatus = table.checkStatus(obj.config.id);
	    switch(obj.event){
		    case 'clearData':
	        basic.ServiceTable.myTable.reload({
                data: []
            });
		    break;
		    case 'test':
		    break;
	     
	    };
	});
});

