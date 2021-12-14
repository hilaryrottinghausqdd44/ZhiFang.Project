/**
	@name：已选选项目
	@author：liangyl	
	@version 2021-05-19
 */
layui.extend({
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
		
	//获取项目列表数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true';
    var table_ind=null;
	var listtable = {
		//参数配置
		config:{
            page: false,
			limit: 500,
			loading : true,
			/**项目类型,0-全部项目,1-医嘱项目，2-组合项目*/
	        type:'0' ,
			defaultOrderBy: [{property:'LBSectionItem_LBSection_DispOrder',direction:'ASC'},{property:'LBSectionItem_LBItem_DispOrder',direction:'ASC'}],
			cols:[[
				{type: 'checkbox', fixed: 'left'},
				{field: 'LBSectionItem_LBItem_Id',width: 60,title: '项目id',sort: true,hide: true},
                {field:'LBSectionItem_LBItem_CName', minWidth:150,flex:1, title: '项目名称', sort: true},
				{field:'LBSectionItem_LBItem_SName', width:150, title: '项目简称', sort: true},
				{field:'LBSectionItem_LBItem_IsOrderItem', width:150, title: '医嘱项目', sort: true, templet:function(record){
					var v = record["LBSectionItem_LBItem_IsOrderItem"];
				    if(v=='true')v='<b>医嘱</b>';
				    else  v='';
	                return v;
				}},
				{field:'LBSectionItem_LBItem_UseCode', width:150, title: '用户代码', sort: true},
				{field:'LBSectionItem_LBItem_PinYinZiTou', width:100, title: '拼音字头', sort: true},
				{field:'LBSectionItem_LBItem_GroupType', minWidth:100,flex:1, title: '是否组合项目', sort: true,hide:true}
			]],
			text: {none: '暂无相关数据' },
			done: function (res, curr, count) {
				res.data.forEach(function (item, index) {
					//项目名称背景色
					if (item.LBSectionItem_LBItem_GroupType == '1'){
						//背景色-绿色
						$('div[lay-id="testform_table"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LBSectionItem_LBItem_CName"]').
						css('background-color', '#F08080;');
					}
					//项目简称背景色
					if (item.LBSectionItem_LBItem_IsOrderItem == 'true'){
						//背景色-绿色
						$('div[lay-id="testform_table"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LBSectionItem_LBItem_SName"]').
						css('background-color', '#3CB371;');
					}
					//医嘱背景色
					$('div[lay-id="testform_table"]').
					find('tr[data-index="' + index + '"]').
					find('td[data-field="LBSectionItem_LBItem_IsOrderItem"]').
					css('background-color', '#3CB371;');
				});
			}
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
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
		},me.config,listtable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj){
		var  me = this,
    		cols = me.config.cols[0],
    		params =[],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL;
	    //小组Id
		if(whereObj.SectionID) {
			params.push("lbsectionitem.LBSection.Id=" + me.SectionID + "");
		}
		if(me.config.type=='1'){//按医嘱项目查询
			params.push("lbsectionitem.LBItem.IsOrderItem=1");
		}
		if(me.config.type=='2'){//按组合项目查询
			params.push("lbsectionitem.LBItem.GroupType=1");
		}
		if(params.length > 0) {
			where+= ' and '+ params.join(' and ');
		}
        var obj ={"where":where};
		
		
		me.instance.reload({
			url:url,
			where:$.extend({},obj,{
				fields:fields.join(','),
				sort:JSON.stringify(me.config.defaultOrderBy)
			})
		});
	};
	
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
		//监听查询，小组列表
	    form.on('submit(search)', function (data) {
	    	result.loadData({});
	    });
	    $("input[name='search_text']").bind("input propertychange",  function() {
	    	var list = table_ind.DATA_LIST;
	    	if($(this).val()){
	    		var data = table_ind.DATA_LIST.length>0 ? table_ind.DATA_LIST  : table_ind.table.cache.table;
			    var list = me.searchList($(this).val(),data);
	    	}
			
			table_ind.instance.reload({data:list});

		});
	};/*
	 * 模糊查询一个数组
	 */
   Class.pt.searchList = function(str, container) {
    var newList = [];
    //新的列表
    var startChar = str.charAt(0);
    //开始字符
    var strLen = str.length;
    //查找符串的长度


    for (var i = 0; i < container.length; i++) {
        var obj = container[i];
        var isMatch = false;
        for (var p in obj) {
            if ( typeof (obj[p]) == "function") {
                obj[p]();
            } else {
                var curItem = "";
                if(obj[p]!=null){
                    curItem = obj[p];
                }
                for (var j = 0; j < curItem.length; j++) {
                    if (curItem.charAt(j) == startChar)//如果匹配起始字符,开始查找
                    {
                        if (curItem.substring(j).substring(0, strLen) == str)//如果从j开始的字符与str匹配，那ok
                        {
                            isMatch = true;
                            break;
                        }
                    }
                }
            }
        }
        if (isMatch) {
            newList.push(obj);
        }
    }
    return newList;
};
	
	//主入口
	listtable.render = function(options){
		var me = new Class(options);
		table_ind = uxtable.render(me.config);
		
		table_ind.loadData = me.loadData;
		//初始化
		table_ind.instance.reload({data:[]});
        me.initListeners(table_ind);
		return table_ind;
	};
	//暴露接口
	exports('listtable',listtable);
});