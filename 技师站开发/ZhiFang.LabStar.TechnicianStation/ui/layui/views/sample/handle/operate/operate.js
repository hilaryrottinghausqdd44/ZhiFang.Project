/**
   @Name：操作记录
   @Author：GHX
   @version 2021-05-20
 */
layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'element', 'layer','table'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
		table = layui.table,
        uxutil = layui.uxutil;
    var app = {};
	app.url={
		selectUrl:uxutil.path.ROOT+'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateByHQL?isPlanish=true'
	};
	app.defaultOrderBy=[{property:'LisOperate_DataAddTime',direction:'ASC'}];
	//get参数
	app.paramsObj = {		
		TestFormId:null,
	};
    //初始化
    app.init = function () {
        var me = this;
        me.getParams();
        me.initListeners();
		me.initTable();
    };
	//获得参数
	app.getParams = function() {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.TESTFORMID) {
			me.paramsObj.TestFormId = params.TESTFORMID;
		}
	};
    //监听
    app.initListeners = function () {
        var me = this;
       
    };  
	 app.initTable = function(){
		var me = this;
		var url = me.url.selectUrl+"&sort="+JSON.stringify(me.defaultOrderBy);
		url += "&where=lisoperate.OperateObject='LisTestForm' and lisoperate.OperateObjectID=" + me.paramsObj.TestFormId;
		 table.render({
		 	elem: '#operateTable',
		 	height: 'full-10',
		 	size: 'sm',
		 	page: true,
		 	//data: data,
		 	url: url,
		 	cols:[[ 
		 		 {
		 			field: 'LisOperate_Id',	title: '记录',minWidth: 125,hide: true,	sort: false
		 		} , {
		 			field: 'LisOperate_OperateType',	title: '操作类型',minWidth: 120,	sort: false
		 		} , {
		 			field: 'LisOperate_OperateTypeID',	title: '类型编码',minWidth: 80,	sort: false
		 		} , {
		 			field: 'LisOperate_DataAddTime',	title: '操作时间',minWidth: 130,	sort: false
		 		} , {
		 			field: 'LisOperate_OperateMemo',	title: '说明',minWidth: 120,	sort: false
		 		}   
		 	]],
		 	limit: 50,
		 	limits:[50,100,150,200,250,300],
		 	autoSort: true, //禁用前端自动排序
		 	text: {
		 		none: '暂无相关数据'
		 	},
		 	response: function() {
		 		return {
		 			statusCode: true, //成功状态码
		 			statusName: 'code', //code key
		 			msgName: 'msg ', //msg key
		 			dataName: 'data' //data key
		 		}
		 	},
		 	parseData: function(res) { //res即为原始返回的数据
		 		if (!res) return;
		 		var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
		 		return {
		 			"code": res.success ? 0 : 1, //解析接口状态
		 			"msg": res.ErrorInfo, //解析提示文本
		 			"count": data.count || 0, //解析数据长度
		 			"data": data.list || []
		 		};
		 	},
		 	done: function(res, curr, count) {	
		 		if(me.config.loadindex){
		 			layer.close(me.config.loadindex);
		 		}
		 	}
		 });
	 };
    //初始化
    app.init();
});