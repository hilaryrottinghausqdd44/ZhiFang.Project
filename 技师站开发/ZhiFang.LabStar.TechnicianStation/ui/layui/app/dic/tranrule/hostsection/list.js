/**
	@name：站点类型对应小组
	@author：liangyl
	@version 2020-08-13
 */
layui.extend({
	tableSelect: '../src/tableSelect/tableSelect'
}).define(['uxutil','uxtable','tableSelect'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		tableSelect=layui.tableSelect,
		uxtable = layui.uxtable;
		
	//获取站点类型对应小组列表数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleHostSectionByHQL?isPlanish=true';
	//获取站点类型列表数据
	var GET_HOSTTYPE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeByHQL?isPlanish=true';
    //检验小组查询服务
	var GET_SECTION_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true";

    //检验小组枚举自定义,方便匹配小组名称
    var ENUM = {};
    //所有检验小组数据
    var SECTION_DATA_LIST =[];
	//列表实例
	var table_ind = null;
		//最大高度
	var win = $(window),
		maxHeight = win.height();
	var HostSectionList = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultOrderBy:"[{property: 'LBTranRuleHostSection_DispOrder',direction: 'ASC'}]",
			cols:[[
				{type: 'checkbox', fixed: 'left'},
				{field:'LBTranRuleHostSection_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBTranRuleHostSection_SectionID',title:'小组名称',minWidth:180,flex:1,templet: function (data) {
					var v = data.LBTranRuleHostSection_SectionID;
					if(ENUM != null)v = ENUM[v];
                    return v;
                }},
				{field:'LBTranRuleHostSection_DispOrder',title:'显示次序',width:100,hide:true}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			afterRender:function(that){
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
				}
			}
		},me.config,HostSectionList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL;
		
		var whereObj = {};
		if($('#BHostTypeID').val()){
			whereObj={'where' : 'lbtranrulehostsection.HostTypeID='+$('#BHostTypeID').val()};
		}
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
		});
	};
	
	Class.pt.initListeners= function(){
		var me = this;
		    //下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
	    $("input.layui-input+.layui-icon").on('click', function () {
	        if (!$(this).hasClass("myDate")) {
	            $(this).prev('input.layui-input')[0].click();
	            return false;//不加的话 不能弹出
	        }
	    });
		//初始化站点类型下拉框
		me.HostTypeList('BHostTypeCName','BHostTypeID');
		//检验小组数据加载
		me.SectionList(function(list){
			for(var i=0;i<list.length;i++){
				ENUM[list[i].LBSection_Id] = list[i].LBSection_CName;
			}
		});
		//按钮事件
		var active = {
			add: function() {//新增关系、
				if(!$('#BHostTypeID').val()){
					layer.msg('请先选择站点类型');
					return false;
				}
			    me.openAddLink();
			},
			del: function() {//删除
				me.onDelClick();
			}
		};
		$('.TranRuleHostSection .layui-btn').on('click', function() {
			var type = $(this).data('type');
			active[type] ? active[type].call(this) : '';
		});
	};
	 //初始化站点类型下拉选择项
	Class.pt.HostTypeList = function(CNameElemID, IdElemID){
		var CNameElemID = CNameElemID || null,
        IdElemID = IdElemID || null;
        var fields = ['Id','CName'],
			url = GET_HOSTTYPE_LIST_URL + "&where=bhosttype.IsUse=1";
		url += '&fields=BHostType_' + fields.join(',BHostType_');
        if (!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'BHostType_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'bhosttype.CName',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '站点类型名称',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: maxHeight-150,
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'numbers', title: '行号' },
                    { field: 'BHostType_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'BHostType_CName', width: 200, title: '站点类型名称', sort: false }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["BHostType_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["BHostType_Id"]);
					table_ind.loadData();
                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
                }
            }
        });
	};
	//弹出新增
	Class.pt.openAddLink= function(){
		var me = this;
	    var win = $(window),
			maxHeight = win.height()-80,
			height = maxHeight > 480 ? maxHeight : 480;
		layer.open({
            type: 2,
            area: ['850px', '90%'],
            fixed: false,
            maxmin: false,
            title:'新增站点类型对应小组',
            content: 'hostsection/transfer/app.html?HostTypeID='+$('#BHostTypeID').val()+'&t='+ new Date().getTime()
        });
	};
	  //获取所有小组名称
	Class.pt.SectionList = function(callback){
		var fields = ['LBSection_Id','LBSection_CName'],
			url = GET_SECTION_LIST_URL;
		url += '&fields='+fields.join(',');
		
		uxutil.server.ajax({
			url:url,
			async:false
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				SECTION_DATA_LIST =list; 
				callback(list);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	};
	//主入口
	HostSectionList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
		me.initListeners();
		return result;
	};
	
    //传递数据给子窗体,已设置的小组，用于过滤
	function ChildEmpData(){
		var arr = table_ind.table.cache.hostsection_table;
		return arr;
	}
	//新增保存后返回数据
	function afterUpdateAddLink(){
		table_ind.loadData();
	}
	window.ChildEmpData = ChildEmpData;
	window.afterUpdateAddLink = afterUpdateAddLink;
	//暴露接口
	exports('HostSectionList',HostSectionList);
});