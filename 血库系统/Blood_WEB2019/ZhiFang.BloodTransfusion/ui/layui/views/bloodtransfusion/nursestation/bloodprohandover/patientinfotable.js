layui.extend({
	uxutil:'ux/util',
	dataadapter: 'ux/dataadapter',
	bloodsconfig: 'config/bloodsconfig'
}).define(['table', 'uxutil', 'dataadapter', 'util', 
   'bloodsconfig'], function(exports){
	"use strict";
	var $ = layui.jquery;
	var util = layui.util;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var bloodsconfig = layui.bloodsconfig;
	//当前用户
    var userInfo = bloodsconfig.getCurOper();
    var empID = userInfo.empID;
	var empName = userInfo.empName;	
	//当前操部门
    var deptId = uxutil.cookie.get(uxutil.cookie.map.DEPTID) || "";
    var deptName = uxutil.cookie.get(uxutil.cookie.map.DEPTNAME) || "";

    var urlParams = uxutil.params.get() || {};
    var admId = urlParams["admId"] ? urlParams["admId"] : ""; //就诊号或者登记号
	//代理对象
	var patientInfotTableProxy = {
	        searchInfo: {
	        	isLike: true,
	        	fields:[]
	        },
	        
	        config: {
	        	elem: '#patient_table',
			    id: 'patient_table',
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
			    selectUrl: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormVOOfHandoverByHQL?isPlanish=true',
			    cols:[
				        [{
			           		field:'BloodBReqFormVO_AdmID', 
			           		sort: true, 
			           		width: 120, 
			           		title: '登记号'			           	
			            },{
			           		field:'BloodBReqFormVO_PatNo', 
			           		sort: true, 
			           		width: 120, 
			           		title: '住院号'				           	
			            },{
			           		field:'BloodBReqFormVO_CName', 
			           		sort: true, 
			           		width: 100, 
			           		title: '姓名'				           	
			            },{
			           		field:'BloodBReqFormVO_Sex', 
			           		sort: true, 
			           		width: 70, 
			           		title: '性别'				           	
			            },{
			           		field:'BloodBReqFormVO_AgeALL', 
			           		sort: true, 
			           		width: 70, 
			           		title: '年龄'				           	
			            },{
			           		field:'BloodBReqFormVO_DeptNo', 
			           		sort: true, 
			           		width: 120, 
			           		title: '科室'			           	
			            },{
			           		field:'BloodBReqFormVO_Bed', 
			           		sort: true, 
			           		width: 100, 
			           		title: '床号'			           	
			            },
				        {
			           		field:'BloodBOutForm_Id', 
			           		sort: true, 
			           		width: 130, 
			           		hide: true,
			           		title: '发血主单(扫码使用)'					        	
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
			    	patientInfotTable.doAutoSelect(me.table, 0);
			    }
		    }
	    };
	    
	//构造器,通过代理对象生成table对象
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, patientInfotTableProxy.config, me.config, options);
		var inst = $.extend(true, {}, patientInfotTableProxy, me); // table,
		inst.config.url = inst.config.selectUrl;
		inst.config.where = inst.getWhere();
		return inst;
	}; 
	
	//获取当前日期几天前的日期
    patientInfotTableProxy.getDateBeforeByday = function(day, format){
	    var curTime = new Date().getTime();
	    var startDate = curTime - (day * 3600 * 24 * 1000);
	    return util.toDateString(startDate, format);
    };
	//获取查询Fields
	patientInfotTableProxy.getFields = function(isString) {
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
	patientInfotTableProxy.getLikeWhere = function(value) {
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
		}
		return where.join(' or ');
	};   
	
	//设置默认的查询where
	patientInfotTableProxy.setDefaultWhere = function(where) {
		var me = this;
		var defWhere = where ? where + ' and ': '';
		var checkdate = me.getDateBeforeByday(30, 'yyyy-MM-dd');
		//由于审核时间没有启用所以使用操作时间
		defWhere = defWhere + " bloodboutform.OperTime >= '" + checkdate + "'";
		defWhere = defWhere + " and (bloodboutform.HandoverCompletion <> 3 or bloodboutform.HandoverCompletion is null) ";
		//修改如果存在就诊id也叫登记号、患者id
		if (admId){
		    defWhere = defWhere + " and bloodbreqform.AdmID = " + "'" + admId + "'";
		} else {
			defWhere = defWhere + " and bloodbreqform.DeptNo = " + deptId;	
		};
		me.config.defaultWhere = defWhere;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	patientInfotTableProxy.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	
	//设置外部传入的查询where
	patientInfotTableProxy.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	
	//获取查询where
	patientInfotTableProxy.getWhere = function() {
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
			"where": where,
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
	patientInfotTableProxy.doAutoSelect = function(curTable, rowIndex) {
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
	
	//跟据数据填充表格
	patientInfotTableProxy.loaddata = function(data){
		var me = this;
		me.config.url = '';
		me.config.data = data;
		var tableIns = table.render(me.config);
		//me.doAutoSelect(tableIns.config.table, 0); ///不触发单击事件
		me.config.data = null;
		me.config.url = me.config.selectUrl; //恢复url
	};
	
	//根据扫描血袋返回数据显示病人信息
	patientInfotTableProxy.showPatinfoByScanbag = function(res){
		var me = this;
		var data = [];
		var row = {};
		res = res || [];
		//if (res.length < 0) return;
		for(var i = 0; i < res.length; i++)
		{
			row = new Object();
			row["BloodBReqFormVO_AdmID"] = res[i]["BloodBOutItem_BloodBReqForm_AdmID"];
			row["BloodBReqFormVO_PatNo"] = res[i]["BloodBOutItem_BloodBReqForm_PatNo"];
			row["BloodBReqFormVO_CName"] = res[i]["BloodBOutItem_BloodBReqForm_CName"];
			row["BloodBReqFormVO_Sex"] = res[i]["BloodBOutItem_BloodBReqForm_Sex"];
			row["BloodBReqFormVO_AgeALL"] = res[i]["BloodBOutItem_BloodBReqForm_AgeALL"];
			row["BloodBReqFormVO_DeptNo"] = res[i]["BloodBOutItem_BloodBReqForm_DeptNo"];
			row["BloodBReqFormVO_Bed"] = res[i]["BloodBOutItem_BloodBReqForm_Bed"];
			row["BloodBOutForm_Id"] = res[i]["BloodBOutItem_BloodBOutForm_Id"];
			data.push(row);			
		}
		me.loaddata(data);		
	};
	
	//核心入口
	patientInfotTableProxy.render = function(options) {
		var me = this;
		me.setDefaultWhere();  //按默认条件查询数据
		me.config = $.extend({}, me.config, options);
		me.tableIns = table.render(me.config);
	};
	
	//暴露接口
	var patientInfotTable = new Class();
	//这里的patientInfotTable跟extend定义必须一致
	exports("patientInfotTable", patientInfotTable);	    
});
