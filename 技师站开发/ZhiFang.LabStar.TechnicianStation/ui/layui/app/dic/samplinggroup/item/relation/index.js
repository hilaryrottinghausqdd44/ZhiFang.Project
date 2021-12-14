/**
	@name：项目与采用组关系
	@author：liangyl	
	@version 2019-09-30
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table',
	itemtable:'app/dic/samplinggroup/item/relation/itemlist',
	samplinggroupitemtable:'app/dic/samplinggroup/item/relation/sgitemlist',
	samplinggrouptable:'app/dic/samplinggroup/item/relation/grouplist',
	goupitemtable:'app/dic/samplinggroup/item/relation/goupitemlist',
	sigrouptable:'app/dic/samplinggroup/item/relation/sigrouplist',
	tableSelect: '../src/tableSelect/tableSelect'
}).use(['samplinggrouptable','itemtable','samplinggroupitemtable','goupitemtable','sigrouptable','uxutil','table','form','element','tableSelect'],function(){
	var $ = layui.$,
		element = layui.element,
		table = layui.table,
		form = layui.form,
		uxutil = layui.uxutil,
//		dictselect = layui.dictselect,
        tableSelect = layui.tableSelect,
		itemtable = layui.itemtable,
		samplinggroupitemtable = layui.samplinggroupitemtable,
		goupitemtable = layui.goupitemtable,
		sigrouptable = layui.sigrouptable,
		samplinggrouptable = layui.samplinggrouptable;
    
    //自定义变量
    var config ={
    	//第一行项目列表(没有设置采样组的项目信息)
    	oneRowItemTable:null,
    	//第二行采样组项目列表(采样组和项目关系.单个)
    	twoRowItemTable:null,
    	//第三行项目列表(采样组和项目关系.多个)
    	threeRowItemTable:null,
    	//第一行采样组列表
    	oneRowSGroupTable:null,
    	//第二行采样组列表
    	twoRowSGroupTable:null,
        //第三行采样组和项目关系列表
        threeRowSGroupTable:null,
        
        //第一行项目列表选择行数据(没有设置采样组的项目信息)
    	oneRowItemCheckRowData:[],
         //第一行采样组列表选择行数据(没有设置采样组的项目信息)
    	oneRowSGroupCheckRowData:[],
    	//第二行项目列表选择行数据(没有设置采样组的项目信息)
    	twoRowItemCheckRowData:[],
         //第二行采样组列表选择行数据(没有设置采样组的项目信息)
    	twoRowSGroupCheckRowData:[],
    	//第三行项目列表选择行数据
    	threeRowItemCheckRowData:[],
    	 //第三行采样组列表选择行数据(没有设置采样组的项目信息)
    	threeRowSGroupCheckRowData:[],
    	//新增和删除采样组项目
    	addUrl: uxutil.path.ROOT  + '/ServerWCF/LabStarPreService.svc/LS_UDTO_AddDelLBSamplingItem',
        //删除采样组项目
        delUrl: uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBSamplingItem'
    };
	
	//检验小组查询服务
	var GET_SECTION_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true";
	//检验项目查询服务
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemByHQL?isPlanish=true";
    var height = $(document).height()-105;
    if(height<400)height=400;
    //第一行列表数据加载
    var oneRow={
    	//第一行第一个项目列表(没有设置采样组的项目信息)
    	iniItemTable : function(){
    	    var obj = {
    	   	    elem:'#onerow-item-table',
		        title:'所有项目',
		        height:height/3,
		        size: 'sm', //小尺寸的表格
		        defaultOrderBy: JSON.stringify([{property: 'LBItem_DispOrder',direction: 'ASC'}]),
		        done: function(res, curr, count) {
					if(count>0){
						//默认选择第一行
						var rowIndex = 0;
						 if(config.twoRowItemCheckRowData.length>0){
						 	for (var i = 0; i < res.data.length; i++) {
				                if (res.data[i].LBItem_Id == config.twoRowItemCheckRowData[0].LBItem_Id) {
				              	    rowIndex=res.data[i].LAY_TABLE_INDEX;
				              	  break;
				                }
				            }
						}
			            //默认选择行
					    doAutoSelect(this,rowIndex);
					    //采样管颜色（背景）
					    var that = this.elem.next();
			            for(var i=0;i<res.data.length;i++){
			            	if(res.data[i].LBSamplingGroup_LBTcuvete_ColorValue){
			                    that.find(".layui-table-box tbody tr[data-index='" + i + "']").find('td:eq(2)').css("background-color", res.data[i].LBSamplingGroup_LBTcuvete_ColorValue);
			            	}
			            }
					}else{
						config.oneRowItemCheckRowData = [];
					}
				}
    	    };
    	    config.oneRowItemTable =  itemtable.render(obj);
    	},
    	//第一行第一个采样组列表
    	iniSGroupTable :function(){
    		var obj = {
    	   	   elem:'#onerow-samplinggroup-table',
		       title:'采样组',
		       height:height/3,
		       size: 'sm', //小尺寸的表格
		       defaultOrderBy: JSON.stringify([{property: 'LBSamplingGroup_DispOrder',direction: 'ASC'}]),
		       done: function(res, curr, count) {
					if(count>0){
						var filter = this.elem.attr("lay-filter");
						//默认选择第一行
						var rowIndex = 0;
			            //默认选择行
					    doAutoSelect(this,rowIndex);
					    //采样管颜色（背景）
					    var that = this.elem.next();
			            for(var i=0;i<res.data.length;i++){
			            	if(res.data[i].LBSamplingGroup_LBTcuvete_ColorValue){
			                    that.find(".layui-table-box tbody tr[data-index='" + i + "']").find('td:eq(2)').css("background-color", res.data[i].LBSamplingGroup_LBTcuvete_ColorValue);
			            	}
			            }
					}else{
						config.oneRowSGroupCheckRowData = [];
					}
				}
    	   };
    	   config.oneRowSGroupTable = samplinggrouptable.render(obj);
    	}
    };
    
    //第二行列表数据加载
    var twoRow={
    	//第一行第一个采样组项目列表(采样组和项目关系.单个)
    	iniSGItemTable : function(){
    	   var obj = {
    	   	   elem:'#tworow-sgitem-table',
		       title:'采样组项目',
		       height:height/3,
		        //单个采样组项目
		       isMulti:false,
		       size: 'sm', //小尺寸的表格
		       done: function(res, curr, count) {
					if(count>0){
						var filter = this.elem.attr("lay-filter");
						//默认选择第一行
						var rowIndex = 0;
						if(config.oneRowItemCheckRowData.length>0){
							for (var i = 0; i < res.data.length; i++) {
				                if (res.data[i].LBItem_Id == config.oneRowItemCheckRowData[0].LBItem_Id) {
				              	    rowIndex=res.data[i].LAY_TABLE_INDEX;
				              	  break;
				                }
				            }
						}
			            //默认选择行
					    doAutoSelect(this,rowIndex);
				    }else{
						config.twoRowItemCheckRowData = [];
					}
				}
    	   };
    	   config.twoRowItemTable = goupitemtable.render(obj);
    	   
    	},
    	//第一行第一个采样组列表
    	iniSGroupTable : function(){
    		var obj = {
    	   	   elem:'#tworow-samplinggroup-table',
		       title:'采样组',
		       height:height/3,
		       size: 'sm', //小尺寸的表格
		       defaultOrderBy: JSON.stringify([{property: 'LBSamplingItem_LBSamplingGroup_DispOrder',direction: 'ASC'}]),
		       done: function(res, curr, count) {
					if(count>0){
						 //默认选择第一行
						var rowIndex = 0;
					    doAutoSelect(this,rowIndex);
					    //采样管颜色（背景）
					    var that = this.elem.next();
			            for(var i=0;i<res.data.length;i++){
			            	if(res.data[i].LBSamplingItem_LBSamplingGroup_LBTcuvete_ColorValue){
			                    that.find(".layui-table-box tbody tr[data-index='" + i + "']").find('td:eq(2)').css("background-color", res.data[i].LBSamplingItem_LBSamplingGroup_LBTcuvete_ColorValue);
			            	}
			            }
					}else{
						config.twoRowSGroupCheckRowData = [];
						config.twoRowSGroupTable.instance.reload({data:[]});
					}
				}
    	   };
    	   config.twoRowSGroupTable =  sigrouptable.render(obj);
    	}
    };
    
     //第三行列表数据加载
    var threeRow={
    	//第三行第一个项目列表(采样组和项目关系.多个)
    	iniItemTable : function(){
    	   var obj = {
    	   	   elem:'#threerow-item-table',
		       title:'所有项目',
		       height:height/3,
		       //多个采样组项目
		       isMulti:true,
		       size: 'sm', //小尺寸的表格
		       defaultOrderBy: JSON.stringify([{property: 'LBItem_DispOrder',direction: 'ASC'}]),
		       done: function(res, curr, count) {
					if(count>0){
						var filter = this.elem.attr("lay-filter");
						//默认选择第一行
						var rowIndex = 0;
			            //默认选择行
					    doAutoSelect(this,rowIndex);
					}
					else{
						config.threeRowItemCheckRowData = [];
						if(config.threeRowSGroupTable)config.threeRowSGroupTable.clearData();
					}
				}
    	   };
    	  config.threeRowItemTable = goupitemtable.render(obj);
    	},
    	//第三行第2个采样组列表
    	iniSGroupItemTable : function(){
    		var obj = {
    	   	   elem:'#threerow-sgroupitem-table',
		       title:'采样组项目',
		       height:height/3,
		       size: 'sm', //小尺寸的表格
		       defaultOrderBy:"[{property: 'LBSamplingItem_DispOrder',direction: 'ASC'}]",
		       cols:[[
//				    {type: 'numbers',title: '行号',fixed: 'left'},
					{field:'LBSamplingItem_LBSamplingGroup_CName',title:'采样组名称',width:150,sort:true},
					{field:'LBSamplingItem_IsDefault',title:'是否缺省',width:100,sort:true,align:'center', templet: '#switchTpl'},
					{field:'LBSamplingItem_MinItemCount',title:'最小项目数',width:130,sort:true},
					{field:'LBSamplingItem_MustItem',title:'必须项目',width:100,sort:true},
					{field:'LBSamplingItem_LBSamplingGroup_LBTcuvete_CName',title:'采样管',width:90,sort:true},
					{field:'LBSamplingItem_LBSamplingGroup_LBTcuvete_ColorValue',title:'颜色',width:90,hide:true},
					{field:'LBSamplingItem_LBSamplingGroup_Id',title:'采样组编号',width:250,sort:true,hide:true},
					{field:'LBSamplingItem_DispOrder',title:'优先次序',width:100,sort:true,edit:true,hide:true},
					{ title:'优先次序', toolbar: '#barDemo', width:130},
					{field:'LBSamplingItem_Id',title:'采样组项目关系id',width:250,sort:true,hide:true}
			    ]],
		       done: function(res, curr, count) {
					if(count>0){
						 //默认选择第一行
						var rowIndex = 0;
			            //默认选择行
					    doAutoSelect(this,rowIndex);
					    //采样管颜色（背景）
					    var that = this.elem.next();
			            for(var i=0;i<res.data.length;i++){
			            	if(res.data[i].LBSamplingItem_LBSamplingGroup_LBTcuvete_ColorValue){
			                    that.find(".layui-table-box tbody tr[data-index='" + i + "']").find('td:eq(6)').css("background-color", res.data[i].LBSamplingItem_LBSamplingGroup_LBTcuvete_ColorValue);
			            	}
			            }
					}
				}
    	   };
    	   config.threeRowSGroupTable = sigrouptable.render(obj);
    	}
    };
    /***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param that:当前操作实例对象
	 * @param rowIndex: 指定选中的行
	 * */
	var doAutoSelect = function(that, rowIndex) {
		var me = this;	
		var data = table.cache[that.instance.key] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		var filter = that.elem.attr('lay-filter');
		var thatrow = $(that.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
        $(".layui-table-main").animate({
            scrollTop: thatrow.offset().top - $(".layui-table-main").offset().top + $(".layui-table-main").scrollTop()
        }, 200);

		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[that.instance.key][index] = [];
				tr.remove();
				that.instance.scrollPatch();
			},
			updte: {}
		};
		setTimeout(function() {
			layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		
		}, 100);
	};
	 //初始化检验小组下拉选择项
    function SectionList(CNameElemID, IdElemID) {
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
                    
		            config.oneRowItemTable.loadData({},record["LBSection_Id"]);
					config.twoRowItemTable.loadData({},record["LBSection_Id"]);
					config.threeRowItemTable.loadData({},record["LBSection_Id"]);
					
                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
                    var groupid = $('#LBSection_ID').val()
                    config.oneRowItemTable.loadData({},groupid);
					config.twoRowItemTable.loadData({},groupid);
					config.threeRowItemTable.loadData({},groupid);
                }
            }
        });
    }
    //初始化项目下拉选择项
    function ItemList(CNameElemID, IdElemID) {
        var CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
        var fields = ['Id','CName','SName','Shortcode'],
			url = GET_ITEM_LIST_URL + "&where=lbitem.IsUse=1";
		url += '&fields=LBItem_' + fields.join(',LBItem_');
        var height = $('#content').height();
        if (!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBItem_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbitem.CName,lbitem.Shortcode,lbitem.SName',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '项目名称/简称/代码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: height,
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'numbers', title: '行号' },
                    { field: 'LBItem_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBItem_CName', width: 200, title: '小组名称', sort: false },
                    { field: 'LBItem_SName', width: 150, title: '简称', sort: false },
                    { field: 'LBItem_Shortcode', width: 120, title: '快捷码', sort: false }
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
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
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
                    $(elem).val(record["LBItem_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["LBItem_Id"]);
                    ItemLoadData(record["LBItem_Id"]);
                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
			
                }
            }
        });
    }
	 //撤销 
	function onDelClick(id,callback){
		var me = this;
        if(!id)return;
    	var url = config.delUrl+'?id='+ id;
	    layer.confirm('确定撤销采样组项目关系?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				callback(index,data);
			});
        });
	}
	//按项目定位下拉选择，定位
	function ItemLoadData(id){
        var onerowgitemdata = table.cache['onerow-item-table'];
    	var tworowsgitemdata = table.cache['tworow-sgitem-table'];
    	var threerowitemdata = table.cache['threerow-item-table'];
        if(!id)return;
        
        var LAY_TABLE_INDEX = -1;
        var isContinue = true;
        //循环onerowgitemdata判断是否存在项目
        for(var i=0;i<onerowgitemdata.length;i++){
        	if(id == onerowgitemdata[i].LBItem_Id ){
        		LAY_TABLE_INDEX  = onerowgitemdata[i].LAY_TABLE_INDEX;
        		isContinue = false;
        		break;
        	}
        }
        
        if(LAY_TABLE_INDEX!=-1){
        	//默认选择行
			doAutoSelect(config.oneRowItemTable.instance.config,LAY_TABLE_INDEX);
        }
        
        if(!isContinue)return;
        //循环tworowsgitemdata判断是否存在项目
        for(var i=0;i<tworowsgitemdata.length;i++){
        	if(id == tworowsgitemdata[i].LBItem_Id ){
        		var LAY_TABLE_INDEX  = tworowsgitemdata[i].LAY_TABLE_INDEX;
        	    isContinue = false;
				break;
        	}
        }
        if(LAY_TABLE_INDEX!=-1){
        	//默认选择行
			doAutoSelect(config.twoRowItemTable.instance.config,LAY_TABLE_INDEX);
        }
        if(!isContinue)return;
        //循环threerowitemdata判断是否存在项目
        for(var i=0;i<threerowitemdata.length;i++){
        	if(id == threerowitemdata[i].LBItem_Id ){
        		var LAY_TABLE_INDEX  = threerowitemdata[i].LAY_TABLE_INDEX;
        		isContinue = false;
        	    //默认选择行
				break;
        	}
        }
        if(LAY_TABLE_INDEX!=-1){
        	//默认选择行
			doAutoSelect(config.threeRowItemTable.instance.config,LAY_TABLE_INDEX);
        }
	}

    //监听
    function initListeners(){
		//下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
	    $("input.layui-input+.layui-icon").on('click', function () {
	        if (!$(this).hasClass("myDate") && !$(this).hasClass("myPhrase")) {
	            $(this).prev('input.layui-input')[0].click();
	            return false;//不加的话 不能弹出
	        }
	    });
		//选择行
		table.on('row(onerow-item-table)', function(obj){
			config.oneRowItemCheckRowData=[];
			config.oneRowItemCheckRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		});
		//选择行
		table.on('row(onerow-samplinggroup-table)', function(obj){
			config.oneRowSGroupCheckRowData=[];
			config.oneRowSGroupCheckRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		});
		//选择行
		table.on('row(tworow-sgitem-table)', function(obj){
			config.twoRowItemCheckRowData=[];
			config.twoRowItemCheckRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        setTimeout(function() {
				config.twoRowSGroupTable.loadData({},obj.data.LBItem_Id);
			}, 200);
			
		});
		//选择行
		table.on('row(tworow-samplinggroup-table)', function(obj){
			config.twoRowSGroupCheckRowData=[];
			config.twoRowSGroupCheckRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		});
		//选择行
		table.on('row(threerow-item-table)', function(obj){
			config.threeRowItemCheckRowData=[];
			config.threeRowItemCheckRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        setTimeout(function() {
				config.threeRowSGroupTable.loadData({},obj.data.LBItem_Id);
			}, 200);
			
		});
		//选择行
		table.on('row(threerow-sgroupitem-table)', function(obj){
			config.threeRowSGroupCheckRowData=[];
			config.threeRowSGroupCheckRowData.push(obj.data);
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		});
			//监听工具条
		table.on('tool(threerow-sgroupitem-table)', function(obj){
		    var data = obj.data;
		    if (obj.event === 'up') { //上移
	            config.threeRowSGroupTable.move(data.LBSamplingItem_Id, obj.event,function(){
	            	if(config.threeRowItemCheckRowData.length>0)config.threeRowSGroupTable.loadData({},config.threeRowItemCheckRowData[0].LBItem_Id);
	            });
	        } else if (obj.event === 'down') { //下移
	            config.threeRowSGroupTable.move(data.LBSamplingItem_Id, obj.event,function(){
	            	if(config.threeRowItemCheckRowData.length>0)config.threeRowSGroupTable.loadData({},config.threeRowItemCheckRowData[0].LBItem_Id);
	            });
	        }
		});
		//指定
		$('#add').on('click',function(){
			var oneRowItemCheckRowData = config.oneRowItemCheckRowData;
			var oneRowSGroupCheckRowData = config.oneRowSGroupCheckRowData;

			if(oneRowItemCheckRowData.length==0){
	            layer.msg("请选择需要指定的项目行！", { icon:5,anim:6});
	            return;
	        }
			if(oneRowSGroupCheckRowData.length==0){
	            layer.msg("请选择需要指定的采样组行！", { icon:5,anim:6});
	            return;
	        }
			onSaveClick();
		});
		//撤销
		$('#del').on('click',function(){
			if(config.twoRowSGroupCheckRowData.length==0){
				layer.msg("请选择删除的行数据！", { icon: 5, anim: 6 ,time:2000});
				return;
			}
			var LBSamplingItem_Id = config.twoRowSGroupCheckRowData[0].LBSamplingItem_Id;
			onDelClick(LBSamplingItem_Id,function(index,data){
				layer.close(index);
				if(data.success === true) {
					config.twoRowSGroupCheckRowData=[];
					config.twoRowItemCheckRowData=[];
					config.oneRowItemCheckRowData=[];
                    layer.msg("撤销成功！", { icon: 6, anim: 0 ,time:2000});
                    var groupid = $('#LBSection_ID').val()
				    config.oneRowItemTable.loadData({},groupid);
					config.twoRowItemTable.loadData({},groupid);
					
				}else{
					layer.msg("撤销失败！", { icon: 5, anim: 6 });
				}
			});
		});
		//撤销多采样组多项目关系
		$('#delmultiple').on('click',function(){
			if(config.threeRowSGroupCheckRowData.length==0){
				layer.msg("请选择删除的行数据！", { icon: 5, anim: 6 ,time:2000});
				return;
			}
			var LBSamplingItem_Id = config.threeRowSGroupCheckRowData[0].LBSamplingItem_Id;
			onDelClick(LBSamplingItem_Id,function(index,data){
				if(data.success === true) {
					layer.close(index);
					config.threeRowSGroupCheckRowData=[];
					config.twoRowSGroupCheckRowData=[];
					config.threeRowItemCheckRowData=[];
                    layer.msg("撤销成功！", { icon: 6, anim: 0 ,time:2000});
                    var groupid = $('#LBSection_ID').val()
				    config.threeRowItemTable.loadData({},groupid);
					config.twoRowItemTable.loadData({},groupid);
				}else{
					layer.msg("撤销失败！", { icon: 5, anim: 6 });
				}
			});
		});
    }
    //新增实体
    function getAddEntity(){
    	var entity={},addList=[],delIDList=[];
        var DataTimeStamp = [0,0,0,0,0,0,0,0];
        var oneRowSGroupCheckRowData = config.oneRowSGroupCheckRowData[0];
        var oneRowItemCheckRowData = config.oneRowItemCheckRowData[0];

        addList.push({
            LBSamplingGroup: { Id: oneRowSGroupCheckRowData.LBSamplingGroup_Id, DataTimeStamp: DataTimeStamp },
            LBItem: { Id: oneRowItemCheckRowData.LBItem_Id, DataTimeStamp: DataTimeStamp }
        });
    	//第二个如果为true新增时 则判断实体是否已经存在 否则不判断
        var isCheckEntityExist = true;
    	entity={addEntityList:addList,isCheckEntityExist:isCheckEntityExist,delIDList:delIDList};
    	return entity;
    }
     //删除实体
    function getDelEntity(){
    	var entity={},addList=[],delIDList=[];
        
    	//第二个如果为true新增时 则判断实体是否已经存在 否则不判断
        var isCheckEntityExist = true;
    	entity={addEntityList:addList,isCheckEntityExist:isCheckEntityExist,delIDList:delIDList};
    	return entity;
    }
     //保存
	function onSaveClick() {
		var entity = getAddEntity();
		var params = JSON.stringify(entity);
		//显示遮罩层
		var obj = {
			type: "POST",
			url: config.addUrl,
			data: params
		};
		uxutil.server.ajax(obj, function(data) {
			//隐藏遮罩层
			if (data.success) {
				layer.msg('保存成功',{ icon: 6, anim: 6 });
				//单个采样项目列表新增行并定位行
				selectTwoRowItemTable();
			} else {
				if(!data.msg)data.msg='保存失败';
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	
	//第二行单个采样组项目关系表插入行
    function selectTwoRowItemTable(){
        var groupid = $('#LBSection_ID').val()
	    config.oneRowItemTable.loadData({},groupid);
		config.twoRowItemTable.loadData({},groupid);
    }
    
    function init(){
    	oneRow.iniItemTable();
    	oneRow.iniSGroupTable();
    	
    	twoRow.iniSGItemTable();
    	twoRow.iniSGroupTable();
    	
    	threeRow.iniItemTable();
    	threeRow.iniSGroupItemTable();
    	
    	//下拉框初始化
    	SectionList('LBSection_CName','LBSection_ID');//检验小组初始化
    	ItemList('LBItem_CName','LBItem_ID');//检验小组初始化
    	//监听
    	initListeners();
    	$('#content').css("height",$(window).height()-60+'px');
    }
    init();
});