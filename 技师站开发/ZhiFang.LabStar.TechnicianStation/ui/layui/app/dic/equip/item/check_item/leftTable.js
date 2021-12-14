/**
	@name：仪器项目选择左列表
	@author：zhangda
	@version 2019-08-20
 */
layui.extend({
}).define(['table','form', 'uxutil'], function (exports) {
	"use strict";

    var $=layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form;
	var config={data:[]};
	var leftTable = {
		searchInfo: {
			isLike: true,
			fields: ['']
		},
		config: {
			positionId:null,
			elem: '',
			id: "",
			/**默认传入参数*/
			defaultParams: {},
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			/**是否默认加载*/
			defaultLoad:false,
			data:[],
			//原始数据
			oldDataList:[],
			/**列表当前排序*/
			sort: [{
				"property": 'LBItem_DispOrder',
				"direction": 'ASC'
			}],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipItemVOByHQL?isPlanish=true',
			where: "",
			toolbar:'',
			page: false,
			limit: 1000,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            size: 'sm', //小尺寸的表格
			loading : false,
			cols:[[
				{type: 'checkbox',fixed: 'left'},
                { field:'LBEquipItemVO_LBEquipItem_Id',width: 150,title: '仪器项目ID',sort: true,hide:true},
                { field: 'LBEquipItemVO_LBItem_Id', width: 150, title: '项目编号', sort: true, hide: true},
                {field:'LBEquipItemVO_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
			    {field:'LBEquipItemVO_LBItem_SName', width:150, title: '英文名称', sort: true},
                {field:'LBEquipItemVO_LBItem_UseCode', width:150, title: '用户编码', sort: true}
			]],
			text: {none: '暂无相关数据' },
			response: function(){
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			}
		},
		set: function (options) {
			var me = this;
			if (options) me.config = $.extend({}, me.config, options);
		}
	};
	
	//构造器
	var Class = function (options) {
		var me = this;
		me.config = $.extend({}, leftTable.config, me.config, options);
		if(me.config.data.length==0)me.config.url = me.getLoadUrl();
		me = $.extend(true, {}, leftTable,Class.pt, me);// table,
		return me;
	};
	Class.pt = Class.prototype;
	//获取查询Url
	Class.pt.getLoadUrl = function () {
		var me = this,arr = [];
		var url = me.config.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		//默认条件
		if (me.config.defaultWhere && me.config.defaultWhere != '') {
			arr.push(me.config.defaultWhere);
		}
		//内部条件
		if (me.config.internalWhere && me.config.internalWhere != '') {
			arr.push(me.config.config.internalWhere);
		}
		//外部条件
		if (me.config.externalWhere && me.config.externalWhere != '') {
			arr.push(me.config.externalWhere);
		}
		//传入的默认参数处理
		if (me.config.defaultParams) {
            arr.push('lbequip.Id='+me.config.defaultParams.equipID);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		if (where) {
			url += '&where=' + JSON.stringify(where);
		}
		var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
		if (defaultOrderBy && defaultOrderBy.length > 0) url += '&sort='+JSON.stringify(defaultOrderBy);

		return url;
	};
	//获取查询Fields
	Class.pt.getStoreFields = function (isString) {
		var me = this,
			columns = me.config.cols[0] || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};
	//核心入口
	leftTable.render = function (options) {
		var me = this;
		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		//监听
		inst.iniListeners();
		return inst;
	};
	leftTable.getOldData =function(){
		return leftTable.oldDataList;
	};
//	//对外公开-数据加载
//	leftTable.onSearch = function(mytable,sectionID){
//		var me = this;
//      var inst = new Class(me);
//      var where ='lbsection.Id='+sectionID;
//      leftTable.where=where;
//      leftTable.url = inst.getLoadUrl();
//      leftTable.elem = "#"+mytable;
//      leftTable.id = mytable;
//	    table.reload(mytable, {
//	    	url:inst.getLoadUrl(),
//			where: {
//				where:where
//			}
//		});
//	};
//	
	Class.pt.iniListeners =  function(){
		var me = this;
	
	};
	
	
	//暴露接口
	exports('leftTable', leftTable);
});
