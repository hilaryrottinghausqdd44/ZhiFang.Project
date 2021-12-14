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
	var rightTable = {
		leftData:[],
		searchInfo: {
			isLike: true,
			fields: ['lbitem.CName','lbitem.EName','lbitem.SName','lbitem.UseCode']
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
			/**小组ID*/
			sectionID:null,
			//左列表已选择的数据
			leftData:[],
			/**列表当前排序*/
			sort: [{
				"property": 'LBItem_DispOrder',
				"direction": 'ASC'
			}],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemVOByHQL?isPlanish=true',
			//delUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBSectionItem',
			//editUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBSectionItemByField',
			//查项目，
			///selectItemUrl: uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true',
            //检验小组查询服务
			//selectSectionUrl: uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true',
			where: "",
			toolbar:'',
			page: false,
			limit: 1000,
            limits: [10,50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            size: 'sm', //小尺寸的表格
			loading : false,
			data:[],
			cols:[[
                { type: 'checkbox', fixed: 'left' },
                { field: 'LBSectionItemVO_LBItem_Id', width: 150, title: '项目编号', sort: true, hide: true },
                { field:'LBSectionItemVO_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
                { field:'LBSectionItemVO_LBItem_SName', width:150, title: '英文名称', sort: true},
                { field:'LBSectionItemVO_LBItem_UseCode', width:150, title: '用户编码', sort: true}
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
                //过滤掉已选的数据
                if (res.ResultDataValue != "" && res.ResultDataValue != null) {
                    var list = [];
                    if (rightTable.leftData.length == 0) {
                        list = data.list;
                    } else {
                        for (var i = 0; i < data.list.length; i++) {
                            var isExec = false;
                            var LBItem_Id = data.list[i].LBItem_Id;
                            for (var j = 0; j < rightTable.leftData.length; j++) {
                                var lLBItem_Id = rightTable.leftData[j].LBEquipItemVO_LBItem_Id;
                                if (lLBItem_Id == LBItem_Id) {
                                    isExec = true;
                                    break;
                                }
                            }
                            if (!isExec) list.push(data.list[i]);
                        }
                    }
                }
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": list || []
				};
			},
			done: function (res, curr, count) {
				if (rightTable.config.positionId) {
					var flag = false;
					var index = null;
					for (var i = 0; i < res.data.length; i++) {
						if (res.data[i]["LBSectionItemVO_LBItem_Id"] == rightTable.config.positionId) {
							flag = true;
							index = i + 1;
						}
					}
					if (flag) {
						$("#rightTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").addClass('layui-table-click').siblings().removeClass('layui-table-click');
						document.querySelector("#rightTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
					}
					rightTable.config.positionId = null;
				} else {
					$("#rightTable+div .layui-table-body table.layui-table tbody tr:first-child").addClass('layui-table-click').siblings().removeClass('layui-table-click');//选中第一条
				}
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
		me.config = $.extend({}, rightTable.config, me.config, options);
		me.config.url = me.getLoadUrl();
		me = $.extend(true, {}, rightTable,Class.pt, me);// table,
		return me;
	};
	Class.pt = Class.prototype;
	//获取查询Url
	Class.pt.getLoadUrl = function () {
		var me = this,arr = [];
		var url = me.config.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		if(me.config.sectionID) url +='&sectionID='+me.config.sectionID;
		//默认条件
		if (me.config.defaultWhere && me.config.defaultWhere != '') {
			arr.push(me.config.defaultWhere);
		}
		//内部条件
		if (me.config.internalWhere && me.config.internalWhere != '') {
			arr.push(me.config.internalWhere);
		}
		//外部条件
		if (me.config.externalWhere && me.config.externalWhere != '') {
			arr.push(me.config.externalWhere);
		}
		//传入的默认参数处理
        if (me.config.defaultParams) {
            arr.push('lbitem.GroupType=0 and lbitem.IsCalcItem <> 1');
            arr.push('lbsection.Id =' + me.config.defaultParams.sectionID + ' and lbitem.Id not in (select a.LBItem.Id from LBEquipItem a where a.LBEquip.Id =' + me.config.defaultParams.equipID +')');
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		if (where) {
			url += '&where=' + where;
//			url += '&where=' + JSON.stringify(where);
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
	rightTable.render = function (options) {
		var me = this;
		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		//监听
		inst.iniListeners();
		return inst;
	};

    Class.pt.onSearch =  function(){
    	var me = this;
    	table.reload(me.config.id, {
	    	url:me.getLoadUrl(),
			where: {
			}
		});
    };
	Class.pt.iniListeners =  function(){
		var me = this;
		//下拉框数据加载
		//me.loadSection();
        //监听查询，小组列表
	    form.on('submit(search)', function (data) {
	    	//模糊查询框内容
	    	var value = data.field.searchText;
	    	me.config.internalWhere  = me.getSearchWhere(value);
	    	//小组
	    	//var SectionID = data.field.SectionID;
	    	//me.config.sectionID= data.field.SectionID;
	        me.onSearch();
	    });
	};
		/**获取查询框内容*/
	Class.pt.getSearchWhere = function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];
		for (var i = 0; i < len; i++) {
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	};
	//检验小组下拉框数据加载
	//Class.pt.loadSection = function(){
	//	var me = this;
	//	var url = me.config.selectSectionUrl+ '&lbsection.IsUse=1'+
	//	'&fields=LBSection_CName,LBSection_Id';
	//	uxutil.server.ajax({
	//		url:url
	//	},function(data){
	//		if(data){
	//			var value = data[uxutil.server.resultParams.value];
 //               if (value && typeof (value) === "string") {
 //                   if (isNaN(value)) {
 //                       value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
 //                       value = value.replace(/\\"/g, '&quot;');
 //                       value = value.replace(/\\/g, '\\\\');
 //                       value = value.replace(/&quot;/g, '\\"');
 //                       value = eval("(" + value + ")");
 //                   } else {
 //                       value = value + "";
 //                   }
 //               }
	//			var tempAjax = "<option value=''>请选择</option>";
 //               if(!value)return;
 //               for (var i = 0; i < value.list.length; i++) {
 //                   tempAjax += "<option value='" + value.list[i].LBSection_Id + "'>" + value.list[i].LBSection_CName + "</option>";
 //                   $("#SectionID").empty();
 //                   $("#SectionID").append(tempAjax);
                    
 //               }
 //               form.render('select');//需要渲染一下;
	//		}else{
	//			layer.msg(data.msg);
	//		}
	//	});
	//};
	//暴露接口
	exports('rightTable', rightTable);
});
