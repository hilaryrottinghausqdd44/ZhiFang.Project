layui.extend({
	uxutil:'ux/util',
	dataadapter: 'ux/dataadapter'
}).define(['table', 'uxutil', 'dataadapter'], function(exports){
	"use strict";
	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	
	//代理对象
	var boutformtableProxy = {
	        searchInfo: {
	        	isLike: true,
	        	fields:[]
	        },
	        
	        config: {
	        	elem: '#boutform_table',
			    id: 'boutform_table',
	            page: false,
				limit: 10,
				height:'full-150',
				url: '',
				defaultToolbar: ['filter'],
				/**默认数据条件*/
			    defaultWhere: '',
			    /**内部数据条件*/
			    internalWhere: '',
			    /**外部数据条件*/
			    externalWhere: '',
			    selectUrl: uxutil.path.ROOT + '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormOfHandoverByBReqVOHQL?isPlanish=true',
			    cols:[
                        [{
			           		field:'BloodBOutForm_Id', 
			           		sort: true, 
			           		width: 120, 
			           		title: '发血单号'			           	
			            },{
			           		field:'BloodBOutForm_CheckTime', 
			           		sort: true, 
			           		width: 120, 
			           		title: '发血时间'				           	
			            },{
			           		field:'BloodBOutForm_DtlTotal', 
			           		sort: true, 
			           		width: 95, 
			           		title: '血液袋数'				           	
			            },{
			           		field:'BloodBOutForm_HandoverCompletion', 
			           		sort: true, 
			           		width: 100, 
			           		title: '完成度'				           	
			            }]
			    ],
			    response: dataadapter.toResponse(), 
			    parseData: function(res) {
					if (res.success){
						var result = dataadapter.toList(res);
						return result;
					} else{
						layer.msg(res.ErrorInfo);
					}
				},
			    done:function(res, curr, response){
			    	var me = this;
			    	boutformtable.doAutoSelect(me.table, 0);
			    }
		    }		
	};
	
	//构造器,通过代理对象生成table对象
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, boutformtableProxy.config, me.config, options);
		var inst = $.extend(true, {}, boutformtableProxy, me); // table,
		inst.config.url = inst.config.selectUrl;
		inst.config.where = inst.getWhere();
		return inst;
	}; 
	
	//获取查询Fields
	boutformtableProxy.getFields = function(isString) {
		var me = this,
			columns = me.config.cols[0] || [],
			length = columns.length,
			fields = [];
		for(var i = 0; i < length; i++) {
			if(columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};
	
	/**获取查询框内容*/
	boutformtableProxy.getLikeWhere = function(value) {
		var me = this;
		if(me.searchInfo == null) return "";
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for(var i = 0; i < len; i++) {
			if(isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		};
		return where.join(' or ');
	};   
	
	//设置默认的查询where
	boutformtableProxy.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	boutformtableProxy.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	
	//设置Vo条件
	boutformtableProxy.setVoWhere = function(VoWhere){
		var me = this;
		me.config.where = me.getVoWhere(VoWhere);	  	
	};
	
	//获取vowhere
	boutformtableProxy.getVoWhere = function(VoWhere){
		var me = this;
		var where = VoWhere || '';
		where = {
			"bReqVO": where,
			'fields': me.getFields(true).join(',')
		};
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;		
	};
	
	//设置外部传入的查询where
	boutformtableProxy.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	
	//获取查询where
	boutformtableProxy.getWhere = function() {
		var me = this,
			arr = [];
		//默认条件
		if(me.config.defaultWhere && me.config.defaultWhere != '') {
			arr.push(me.config.defaultWhere);
		}
		//内部条件
		if(me.config.internalWhere && me.config.internalWhere != '') {
			arr.push(me.config.config.internalWhere);
		}
		//外部条件
		if(me.config.externalWhere && me.config.externalWhere != '') {
			arr.push(me.config.externalWhere);
		}
		var where = "";
		if(arr.length > 0) where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		where = {
			"bReqVO": where,
			'fields': me.getFields(true).join(',')
		};

		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	
	/***
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
	boutformtableProxy.doAutoSelect = function(curTable, rowIndex) {
		var key = "";
		if (!curTable) return;
		if (curTable.id) key = curTable.id;
		else if (curTable.key) key = curTable.key;
		var data = table.cache[key] || [];
		if (!data || data.length <= 0) return;
		rowIndex = rowIndex || 0;
		var filter = curTable.config.elem.attr('lay-filter');
		var thatrow = $(curTable.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[key][index] = [];
				tr.remove();
				curTable.scrollPatch();
			},
			updte: {}
		};
		//触发单击行事件
		setTimeout(function() {
			layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		}, 300);
	};
	
	//清楚列表数据
	boutformtableProxy.cleardata = function(){
		var me = this;
		var data = [];
		me.config.url = '';
		me.config.data = data;
		var tableIns = table.render(me.config);
		me.config.data = null;
		me.config.url = me.config.selectUrl; //恢复url		
	};
	
	//跟据数据填充表格
	boutformtableProxy.loaddata = function(data){
		var me = this;
		me.config.url = '';
		me.config.data = data;
		var tableIns = table.render(me.config);
		//me.doAutoSelect(tableIns.config.table, 0); //不触发单击事件
		me.config.data = null;
		me.config.url = me.config.selectUrl; //恢复url
	};
	
	//根据扫描血袋返回数据显示发血主单信息
	boutformtableProxy.showOutForminfoByScanbag = function(res){
		var me = this;
		var data = [];
		var row = {};
		res = res || [];
		if (res.length < 0) return;
		row["BloodBOutForm_Id"] = res[0]["BloodBOutItem_BloodBOutForm_Id"];
		row["BloodBOutForm_CheckTime"] = res[0]["BloodBOutItem_BloodBOutForm_CheckTime"];
		row["BloodBOutForm_DtlTotal"] = 1;//没有返回血袋总数，显示为1
		row["BloodBOutForm_HandoverCompletion"] = res[0]["BloodBOutItem_BloodBOutForm_HandoverCompletion"];
		data.push(row);
		me.loaddata(data);		
	};
	
	//核心入口
	boutformtableProxy.render = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, options);
		me.tableIns = table.render(me.config);
	};
	//暴露接口
	var boutformtable = new Class();
	//这里的boutformtable跟extend定义必须一致
	exports("boutformtable", boutformtable);	
});