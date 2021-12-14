layui.extend({
	tableSelect: '../src/tableSelect/tableSelect'
}).define(['uxutil','uxtable','table','form','tableSelect'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		tableSelect = layui.tableSelect,
		uxtable = layui.uxtable;
	
	//获取短语列表数据
	var GET_BPHRASE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBPhraseValue';
	//检验小组查询服务
	var GET_SECTION_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true";
	var righttable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultLoad:false,//是否默认加载
			oldListData:[],//初始化数据
			SectionID:null,//小组
			
			cols:[[
			    {type: 'checkbox',fixed: 'left'},
//			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBPhraseVO_Id',width: 150,title: 'ID',sort: true,hide:true},
				{field:'LBPhraseVO_ObjectID',width: 150,title: 'ObjectID',sort: true,hide:true},
				{field: 'LBPhraseVO_TypeName', title: '短语类型',minWidth: 100,hide:false},
				{field: 'LBPhraseVO_TypeCode', title: 'TypeCode',minWidth: 100,hide:true},
				{field: 'LBPhraseVO_CName', title: '短语名称',minWidth: 100},
	            {field: 'LBPhraseVO_Shortcode',title: '快捷码',minWidth: 80},
	            {field: 'LBPhraseVO_PinYinZiTou',title: '拼音字头',minWidth: 80},
	            {field: 'ID',title: '标记',minWidth: 80,hide:true},
	            {field: 'Tag',title: '已选择(1)',minWidth: 80,hide:true}
			]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {
				var that =this.elem.next();
	            for (var i = 0; i < res.data.length; i++) {
	            	if(res.data[i].Tag == '1'){
	            		that.find(".layui-table-box tbody tr[data-index='" + i + "']").css("color", "#FF4500");
	            		that.find('.layui-table-box tbody th[data-field="1"] input[type="checkbox"]').prop('disabled', true); // 禁止全选
		                let index = res.data[i].LAY_TABLE_INDEX;
		                that.find('.layui-table-box tbody tr[data-index=' + index + '] input[type="checkbox"]').prop('disabled', true); // 禁止部分选择        
		            }
	            }
	            form.render('checkbox');
			}
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
		},me.config,righttable.config,setings);
	};
	
	
	Class.pt = Class.prototype;
	
	 //数据加载
	Class.pt.loadData = function(whereObj,typeName,typeCode){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_BPHRASE_LIST_URL+'?phraseType=SamplePhrase&typeName='+typeName+'&typeCode='+typeCode;
		if($('#LBSection_ID').val()){
			url+='&objectID='+$('#LBSection_ID').val();
		}
		if($('#searchText').val()){
			url+='&otherWhere='+encodeURI(getSearchWhere($('#searchText').val()));
		}
		uxutil.server.ajax({
			url: url
		},function(data){
			if(data.success){
				var list =(data.value || {}).list || [];
				if(list.length>0){
					list = JSON.stringify(list);
				    list = list.replace(/\ LBPhraseVO/g, "LBPhraseVO");
				    list = $.parseJSON(list);
				}
				var arr =[];
				if(me.config.groupItemID)arr = $.parseJSON(me.config.groupItemID);
				var listData =[],data=[];
				for(var i=0;i<list.length;i++){
					var str2= list[i].LBPhraseVO_CName+list[i].LBPhraseVO_TypeName+list[i].LBPhraseVO_TypeCode;
					
					var isAdd = false;
					for(var j=0;j<arr.length;j++){
						var str1= arr[j].LBPhrase_CName+arr[j].LBPhrase_TypeName+arr[j].LBPhrase_TypeCode;
//      	            if(list.indexOf(str1) == -1){  //如果不存在需要匹配的特定字段，则加入

						if(str1 == str2){
							isAdd = true;
							break;
						}
					}
					if(!isAdd && data.indexOf(str2)===-1){
						listData.push(list[i]);
						data.push(str2);
					}
				}
				me.config.oldListData=listData;
                me.instance.reload({data:listData});
			}else{
				layer.msg(data.msg);
			}
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
	};
	//主入口
	righttable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		//下拉框数据加载
    	me.SectionList('LBSection_CName','LBSection_ID');//检验小组初始化

		result.loadData = me.loadData;
		if(me.config.defaultLoad){
			//加载数据
			result.loadData(me.config.where);
		}
		me.initListeners();
	  
		return result;
	};
	//初始化检验小组下拉选择项
    Class.pt.SectionList =  function(CNameElemID, IdElemID) {
        var CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
        var fields = ['Id','CName','SName','Shortcode'],
			url = GET_SECTION_LIST_URL + "&where=lbsection.IsUse=1";
		url += '&fields=LBSection_' + fields.join(',LBSection_');
        var height = $('#content').height();
        if (!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBSection_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbsection.CName,lbsection.Shortcode,lbsection.SName',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '小组名称/简称/代码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: height,
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'LBSection_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBSection_CName', width: 200, title: '小组名称', sort: false },
                    { field: 'LBSection_SName', width: 150, title: '简称', sort: false },
                    { field: 'LBSection_Shortcode', width: 120, title: '快捷码', sort: false }
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
                    $(elem).val(record["LBSection_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["LBSection_Id"]);

                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
                }
            }
        });
    };
		/**获取查询框内容*/
	function getSearchWhere(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var	fields = ['lbphrase.CName','lbphrase.Shortcode'],
			len = fields.length,
			where = [];
		for (var i = 0; i < len; i++) {
			where.push(fields[i] + " like '%" + value + "%'");
		}
		where = "("+where.join(' or ')+")";
		return where;
	};
	//暴露接口
	exports('righttable',righttable);
});