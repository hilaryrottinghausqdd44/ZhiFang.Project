/**
 * @name：小组项目
 * @author：liangyl
 * @version 2021-09-07
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).define(['form','uxutil','uxtable','element'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		element = layui.element,
		form = layui.form,
		MOD_NAME = 'SectionItemList';
	
	//小组项目列表服务地址
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemVOByHQL?isPlanish=true';
    //获取组合项目服务地址
    var GET_SECTION_ITEM_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true';
    //获取仪器服务地址
    var GET_EQUIP_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true';
    //小组项目编辑服务
    var EDIT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBSectionItemByField';
	//组合项目下拉框内容
	var GROUP_ITEM_DATA = [];
	//仪器下拉框内容
	var EQUIP_DATA = [];
	//小组
	var SECTIONID = null;
	//小组名称
	var SECTIONNAME ="";
	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table" style="overflow-y:hidden;">',
			'<div class="layui-btn-group">', 
	          '<button class="layui-btn layui-btn-xs" id="{tableId}-addgroupitem"><i class="iconfont"></i>&nbsp;选择小组项目</button>', 
	          '<button class="layui-btn layui-btn-normal layui-btn-xs" id="{tableId}-addcopy"><i class="iconfont"></i>&nbsp;复制小组项目</button>', 
	          '<button class="layui-btn layui-btn-xs" id="{tableId}-sort"> <i class="iconfont"></i>&nbsp;小组项目排序</button>', 
		'<button class="layui-btn  layui-btn-xs" id="{tableId}-item-save"><i class="iconfont">&#xe713;</i>&nbsp;默认值保存</button>',
		'<button class="layui-btn  layui-btn-xs layui-hide" id="{tableId}-item-template"><i class="iconfont">&#xe713;</i>&nbsp;项目快捷模板</button>',
	        ' </div>', 
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		 '</div>',
	 		' <script type="text/html" id="switchTpl">',
            '<input type="checkbox" name="IsDefult" title="" lay-skin="primary" lay-filter="isdefult" {{ d.LBSectionItemVO_LBSectionItem_IsDefult == "true" ? "checked" : "" }} >',
          '</script>', 
         ' <script type="text/html" id="grouptypeTpl">', 
            '<input type="checkbox" name="GroupType" title="" disabled="disabled" lay-skin="primary" lay-filter="grouptype" {{ d.LBSectionItemVO_LBItem_GroupType == "1" ? "checked" : "" }} >',
        '</script> ',
         ' <script type="text/html" id="groupItemTpl">',    
		    '<select name="groupitem"  lay-filter="groupitem" >',
           '</select>',
		'</script> ',
          '<script type="text/html" id="equipTpl"> ',   
		    '<select name="equip"  lay-filter="equip" >',
            '</select>',
		'</script>', 	 
		'</div>'
	];
	//医嘱单列表
	var SectionItemList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			size:'sm',//小尺寸的表格
			where:{},
			height:null,
			page: true,
			limit: 50,
			limits: [20,50,100, 200, 500, 1000, 1500],
				/**列表当前排序*/
			defaultOrderBy: [{
				"property": 'LBSectionItem_DispOrder',
				"direction": 'ASC'
			},{
				"property": 'LBItem_DispOrder',
				"direction": 'ASC'
			}],
			cols:[[
				{type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBSectionItemVO_LBSectionItem_Id',width: 150,title: '主键',hide:true},
				{field:'LBSectionItemVO_LBItem_Id',width: 150,title: '项目编号',hide:true},
                {field:'LBSectionItemVO_LBItem_CName', minWidth:150,flex:1, title: '项目名称'},
				{field:'LBSectionItemVO_LBItem_SName', width:100,title: '项目简称'},
				{field:'LBSectionItemVO_LBItem_GroupType', width:100, title: '组合项目',align:'center', templet: '#grouptypeTpl'},
				{field:'LBSectionItemVO_LBSectionItem_GroupItemID', minWidth:150,flex:1, title: '默认组合项目', templet: '#groupItemTpl'},
				{field:'LBSectionItemVO_LBSectionItem_EquipID',width:150,  title: '默认仪器', templet: '#equipTpl'},
				{field:'LBSectionItemVO_LBSectionItem_IsDefult', width:90, title: '默认项目',align:'center', templet: '#switchTpl'},
				{field:'LBSectionItemVO_LBSectionItem_DefultValue', width:150, title: '默认值', edit: 'text'},
				{field:'LBSectionItemVO_Tab', width:150, title: '用于判断行是否有修改数据',hide:true},
				{field:'LBSectionItemVO_LBSectionItem_DispOrder', width:100, title: '小组项目排序',hide:true},
				{field:'LBSectionItemVO_LBItem_DispOrder', width:100, title: '项目排序',hide:true},
			]],
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			text: {none: '暂无相关数据' }
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,SectionItemList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,SectionItemList.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
	
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			$("select[name='groupitem']").parent('div.layui-table-cell').css('overflow', 'visible');
            $("select[name='equip']").parent('div.layui-table-cell').css('overflow', 'visible');
            
            $("select[name='groupitem']").empty();
		    $("select[name='groupitem']").append(GROUP_ITEM_DATA);
		  
	        $("select[name='equip']").empty();
		    $("select[name='equip']").append(EQUIP_DATA);

			var that = this.elem.next();
			for(var i=0;i<res.data.length;i++){
				var GroupItemID = res.data[i].LBSectionItemVO_LBSectionItem_GroupItemID;
	        	var EquipID  = res.data[i].LBSectionItemVO_LBSectionItem_EquipID;
				var trRow = that.find(".layui-table-box tbody tr[data-index='" + i + "']");
				$(trRow).find("td").each(function(){
					var fieldName = $(this).attr("data-field");
                    var selectJq = $(this).find("select");
                    if(selectJq.length == 1){
                    	if(GroupItemID ==res.data[i][fieldName]){
                    		$(this).children().children().val(GroupItemID);
                    	}
                    	if(EquipID ==res.data[i][fieldName]){
                    		$(this).children().children().val(EquipID);
                    	}
                    }
				});
            }
            form.render('select');
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g,me.tableId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);
		 //下拉框初始化
		me.initComHtml();
	};
	 //下拉框初始化
    Class.prototype.initComHtml = function(){
    	var me = this;
    	me.getGroupItem(function(list){
			var len = list.length,
				htmls = ['<option value="">请选择</option>'];
			for(var i=0;i<len;i++){
				htmls.push('<option value="' + list[i].LBItem_Id + '">' + list[i].LBItem_CName + '</option>');
			}
			GROUP_ITEM_DATA =htmls.join("");
		});
		me.getEquip(function(list){
			var len = list.length,
				htmls = ['<option value="">请选择</option>'];
			for(var i=0;i<len;i++){
				htmls.push('<option value="' + list[i].LBEquip_Id + '">' + list[i].LBEquip_CName + '</option>');
			}
			EQUIP_DATA =htmls.join("");
		});
    };
	//从服务器获取组合项目
	Class.prototype.getGroupItem = function(callback){
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_SECTION_ITEM_URL,
			type:'get',
//			async:false, 
			data:{
				page:1,
				limit:1000,
				fields:'LBItem_CName,LBItem_Id',
				where:'lbitem.IsUse=1 and lbitem.GroupType=1'
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				callback((data.value ||{}).list || []);
			}else{
				layer.msg(data.msg,{icon:5});
			}
		});
	};
	//从服务器获取仪器
	Class.prototype.getEquip = function(callback){
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_EQUIP_URL,
			type:'get',
//			async:false, 
			data:{
				page:1,
				limit:1000,
				fields:'LBEquip_CName,LBEquip_Id',
				where:'lbequip.IsUse=1'
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				callback((data.value ||{}).list || []);
			}else{
				layer.msg(data.msg,{icon:5});
			}
		});
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
       //默认组合项目选择
        form.on('select(groupitem)', function (data) {
        	//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
			var elem = data.othis.parents('tr');
		  	var dataindex = elem.attr("data-index");
		  	var tableCache = me.uxtable.table.cache[me.tableId]; 
	        //改变后的数据
	        var rowObj = tableCache[dataindex].LBSectionItemVO_Tab;
	        if(rowObj) delete rowObj.LBSectionItemVO_LBSectionItem_GroupItemID;
	        if(!rowObj)rowObj ={};
	  	    $.each(tableCache,function(index,value){
		       	if(value.LAY_TABLE_INDEX==dataindex){
		       		if(data.value)rowObj.LBSectionItemVO_LBSectionItem_GroupItemID=data.value;
		       		value.LBSectionItemVO_Tab=rowObj;
		       		value.LBSectionItemVO_LBSectionItem_GroupItemID = data.value;
		       	}
	       });  
        });
        //仪器
        form.on('select(equip)', function (data) {
        	//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
			var elem = data.othis.parents('tr');
		  	var dataindex = elem.attr("data-index");
		  	var tableCache = me.uxtable.table.cache[me.tableId];
	        //改变后的数据
	        var rowObj = tableCache[dataindex].LBSectionItemVO_Tab;
	        if(rowObj) delete rowObj.LBSectionItemVO_LBSectionItem_EquipID;
	        if(!rowObj)rowObj ={};
	  	    $.each(tableCache,function(index,value){
		       	if(value.LAY_TABLE_INDEX==dataindex){
		       		if(data.value)rowObj.LBSectionItemVO_LBSectionItem_EquipID=data.value;
		       		value.LBSectionItemVO_Tab=rowObj;
		       		value.LBSectionItemVO_LBSectionItem_EquipID = data.value;
		       	}
	       });  
        });
        form.on('checkbox(isdefult)', function (data) {
        	//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
			var elem = data.othis.parents('tr');
		  	var dataindex = elem.attr("data-index");
		  	var tableCache = me.uxtable.table.cache[me.tableId]; 
	        //改变后的数据
	        var rowObj = tableCache[dataindex].LBSectionItemVO_Tab;
	        if(rowObj) delete rowObj.LBSectionItemVO_LBSectionItem_IsDefult;
	        if(!rowObj)rowObj ={};
	  	    $.each(tableCache,function(index,value){
		       	if(value.LAY_TABLE_INDEX==dataindex){
		       		if(data.value)rowObj.LBSectionItemVO_LBSectionItem_IsDefult=data.elem.checked;
		       		value.LBSectionItemVO_Tab=rowObj;
		       		value.LBSectionItemVO_LBSectionItem_IsDefult = data.elem.checked;
		       	}
	        });  
	    });
		//监听单元格编辑
		me.uxtable.table.on('edit('+me.tableId+')', function(obj){
		    var value = obj.value, //得到修改后的值
		        data = obj.data,//得到所在行所有键值
		        field = obj.field; //得到字段
 	        var tableCache = me.uxtable.table.cache[me.tableId]; 
 	        var dataindex = 0;
 	        for(var i=0;i<tableCache.length;i++){
 	        	if(tableCache[i].LBSectionItemVO_LBSectionItem_Id == data.LBSectionItemVO_LBSectionItem_Id){
 	        		dataindex = i;
 	        		break;
 	        	}
 	        }
	        //改变后的数据
	        var rowObj = tableCache[dataindex].LBSectionItemVO_Tab;
	        if(rowObj) delete rowObj.LBSectionItemVO_LBSectionItem_DefultValue;
	        if(!rowObj)rowObj ={};
	  	    $.each(tableCache,function(index,value){
		       	if(value.LAY_TABLE_INDEX==dataindex){
		       		if(data.value)rowObj.LBSectionItemVO_LBSectionItem_DefultValue=value;
		       		value.LBSectionItemVO_Tab=rowObj;
		       	}
	        }); 
    	});
    	//保存修改行
		$('#'+me.tableId+'-item-save').on('click',function(){
			me.onSaveClick();
		});
		//复制小组
	    $('#'+me.tableId+'-addcopy').on('click',function(){
			layer.open({
	            type: 2,
	            area: ['600px', '450px'],
	            fixed: false,
	            maxmin: false,
	            title:'复制小组项目',
	            content: 'item/copy/app.html?sectionID='+SECTIONID,
	            cancel: function (index, layero) {
		        	parent.layer.closeAll('iframe');
	            }
	        });
		});
	    //选择小组项目
	    $('#'+me.tableId+'-addgroupitem').on('click',function(){
			layer.open({
				title:'选择小组项目',
				type:2,
				content: 'item/transfer/app.html?SECTIONID='+SECTIONID+'&SECTIONCNAME='+SECTIONNAME,
				maxmin:true,
				toolbar:true,
				resize:true,
				area:['90%','90%']
			});
		});
        //小组项目排序
         $('#'+me.tableId+'-sort').on('click',function(){
			layer.open({
				title:'小组项目排序',
				type:2,
				content:'item/sort/app.html?t=' + new Date().getTime(),
				maxmin:true,
				toolbar:true,
				resize:true,
				area:['90%','90%'],
				success: function(layero, index){
	       	        var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
	       	        body.find('#sectionID').val(SECTIONID);
		        },
		        cancel: function (index, layero) {
		        	parent.layer.closeAll('iframe');
	            }
			});
        });
		//项目快捷模板
		$('#' + me.tableId + '-item-template').on('click', function () {
			layer.open({
				title: '项目快捷模板维护',
				type: 2,
				content: '../itemtemplate/index.html?SECTIONID=' + SECTIONID,
				maxmin: true,
				toolbar: true,
				resize: true,
				area: ['90%', '90%']
			});
		});
	};
	Class.prototype.updateOne =  function(index,obj){
   		var me = this;
   		setTimeout(function() {
   	        var  id = obj.LBSectionItemVO_LBSectionItem_Id;
   	        var  GroupItemID = obj.LBSectionItemVO_LBSectionItem_GroupItemID;
   	        var  EquipID = obj.LBSectionItemVO_LBSectionItem_EquipID;
   	        var  IsDefult = obj.LBSectionItemVO_LBSectionItem_IsDefult;
   	        var  DefultValue = obj.LBSectionItemVO_LBSectionItem_DefultValue;
            var entity ={
            	Id:id,
            	IsDefult:IsDefult,
            	DefultValue:DefultValue
            };
            if(GroupItemID)entity.GroupItemID=GroupItemID;
            if(EquipID)entity.EquipID=EquipID;
            var fields ="Id,GroupItemID,EquipID,IsDefult,DefultValue";
            var params={entity:entity,fields:fields};
		    params = JSON.stringify(params);
           //显示遮罩层
			var config = {
				type: "POST",
				url: EDIT_URL,
				data: params
			};
   			uxutil.server.ajax(config, function(data) {
				//隐藏遮罩层
				layer.closeAll('loading');
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}				
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					if (me.saveErrorCount == 0){
						layer.msg('保存成功！',{icon:6,time:2000});
						table.reload(itemTable.id);
					}else{
						layer.msg(data.msg,{ icon: 5, anim: 6 });
					}
				}
			})
		}, 100 * index);
   	};
   	//获取修改过的行记录
    Class.prototype.getModifiedRecords =  function(){
    	var me = this,list=[];
    	//获取列表数据
		var tableCache = me.uxtable.table.cache[me.tableId]; 
	    for(var i = 0;i<tableCache.length;i++){
	    	//找到修改过数据的行
	    	if(tableCache[i].LBSectionItemVO_Tab){
	    		list.push(tableCache[i]);
	    	}
	    }
	    return list;
    };
	//保存方法
	Class.prototype.onSaveClick =  function(){
		var me = this;
		var records = me.getModifiedRecords();
		if(records.length==0){
			layer.msg('没有修改的数据不需要保存！');
            return;
		}
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
		//显示遮罩
		if(records.length==0)return;
		var indexs=layer.load();
		//获取列表数据
	    for(var i = 0;i<records.length;i++){
	    	//找到修改过数据的行
	    	if(records[i].LBSectionItemVO_Tab){
	    		me.updateOne(i,records[i]);
	    	}
	    }
	};
    //数据查询
	Class.prototype.onSearch = function (sectionID, Name, ProDLL){
		var me = this,
    		cols = me.tableConfig.cols[0],
			fields = [];
	     SECTIONID = sectionID;
	     SECTIONNAME = Name;
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL+'&fields='+fields.join(',');
        var where ='lbsection.Id='+sectionID;
		var whereObj ={"where":where};
        var page = me.uxtable.instance.config.instance.layPage.find('.layui-laypage-curr>em:last-child').html() || 1;
		me.uxtable.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				page: {
                    curr: page //重新从第 page 页开始
                },
				sort: JSON.stringify(me.tableConfig.defaultOrderBy)
			})
		});
		//大文本显示项目快捷模板
		if (ProDLL == 1) {
			if ($("#" + me.tableId + "-item-template").hasClass("layui-hide")) $("#" + me.tableId + "-item-template").removeClass("layui-hide");
		} else {
			if (!$("#" + me.tableId + "-item-template").hasClass("layui-hide")) $("#" + me.tableId + "-item-template").addClass("layui-hide");
		}

	};
	
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	
	//核心入口
	SectionItemList.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		me.uxtable = uxtable.render(me.tableConfig);
		me.uxtable.instance.reload({
			url: '',
			data:[]
		});
		//监听事件
		me.initListeners();
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,SectionItemList);
});