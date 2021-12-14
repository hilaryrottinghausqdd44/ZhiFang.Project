/**
   @Name：分发规则设置
   @Author：liangyl
   @version 2021-08-12
 */
layui.extend({
	uxutil:'/ux/util',
	uxtable:'/ux/table',
	HostSectionList:'app/dic/tranrule/hostsection/list',//站点类型对应小组
    RuleItemList:'app/dic/tranrule/ruleitemlist',//分发规则项目明细
    RuleList:'app/dic/tranrule/list'//分发规则
}).use(['uxutil','table','HostSectionList','RuleItemList','RuleList'],function(){
	var $ = layui.$,
		uxutil=layui.uxutil,
		HostSectionList = layui.HostSectionList,
		RuleItemList = layui.RuleItemList,
		RuleList = layui.RuleList,
		table = layui.table;
	//站点类型对应小组实例
	var table_ind0=null;
	//分发规则列表实例
	var table_ind1=null;
    //分发规则项目明细实例
	var table_ind2=null;
	
	//选中的站点类型对应小组行
	var CHECK_ROW_DATA = [];
	//分发规则列表行
	var CHECK_ROW_DATA_RULE = [];
	//默认规则类型
	var DEFAULTDRULETYPEID ="";
	//新增数据服务
	var ADD_RULE_TYPE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBTranRuleType';
     //获取分发规则类型
    var GET_RULE_TYPE_URL =uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleTypeByHQL?isPlanish=true';
     //是否是删除规则
    var isDel = false;
	//最大高度
	var win = $(window),
		maxHeight = win.height()-50;
	
	var HOSTSECTIONID = null,//站点类型对应小组ID
	    RULEID=null; //规则ID
	 
	//分发规则类型加载
	ruletype(function(){
		//站点类型对应小组实例初始化
		table_ind0 = HostSectionList.render({
			elem:'#hostsection_table',
	    	title:'站点类型对应小组列表',
	    	height:'full-55',
	    	size: 'sm', //小尺寸的表格
	    	defaultOrderBy: JSON.stringify([{property: 'LBTranRuleHostSection_DispOrder',direction: 'ASC'}]),
	    	done: function(res, curr, count) {
				setTimeout(function(){
					var rowIndex = 0,bo=true;
					for (var i = 0; i < res.data.length; i++) {
		                if (res.data[i].LBTranRuleHostSection_Id == HOSTSECTIONID) {
		              	    rowIndex=i;
		              	    break;
		                }
		            }
					if(count>0){
						$("#hostsection_table+div").find('.layui-table-main tr[data-index="'+rowIndex+'"]').click();
	                }else{
						bo=false;
						CHECK_ROW_DATA = [];
						if(table_ind1)table_ind1.clearData();
						if(table_ind2)table_ind2.clearData();
					}
					isBtnEnableRule(bo);//新增规则和删除按钮是否可用
				},0);
			}
		});
		table_ind0.instance.reload({data:[]});
		//分发规则列表实例初始化
		table_ind1 = RuleList.render({
			elem:'#tranrule_table',
	    	title:'取单时间分类明细列表',
	    	height:'full-55',
	    	size: 'sm', //小尺寸的表格
	    	DEFAULTDRULETYPEID: DEFAULTDRULETYPEID,
	    	done: function(res, curr, count) {
	    		setTimeout(function(){
					var rowIndex = 0,bo=true;
					for (var i = 0; i < res.data.length; i++) {
		                if (res.data[i].LBTranRule_Id == RULEID) {
		              	    rowIndex=i;
		              	    break;
		                }
		            }
					if(count>0){
						$("#tranrule_table+div").find('.layui-table-main tr[data-index="'+rowIndex+'"]').click();
	                    $("#edit").removeClass("layui-btn-disabled").removeAttr('disabled',true);
						$("#del").removeClass("layui-btn-disabled").removeAttr('disabled',true);
					}else{
						bo = false;
						CHECK_ROW_DATA_RULE = [];
						$("#del").addClass("layui-btn-disabled").attr('disabled',true);
						$("#edit").addClass("layui-btn-disabled").attr('disabled',true);
						if(table_ind2)table_ind2.clearData();
						if(CHECK_ROW_DATA.length>0 && CHECK_ROW_DATA[0].LBTranRuleHostSection_SectionID && !isDel){
							table_ind1.rule(CHECK_ROW_DATA[0].LBTranRuleHostSection_SectionID,function(){
								table_ind1.loadData(CHECK_ROW_DATA[0].LBTranRuleHostSection_SectionID);
							});
						}
					}
					isBtnEnableRuleItem(bo);//选择项目按钮是否可用
				},0);
			}
		});
		//分发规则项目明细实例初始化
		table_ind2 = RuleItemList.render({
			elem:'#ruleitem_table',
	    	title:'分发规则项目明细列表',
	    	height:'full-55',
	    	size: 'sm' //小尺寸的表格
		});
		table_ind2.instance.reload({data:[]});
	});
	//站点类型对应小组列表选择行监听
	table_ind0.table.on('row(hostsection_table)', function(obj){
		CHECK_ROW_DATA = [obj.data];
		isDel = false;
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	    //分发规则加载
	    table_ind1.loadData(obj.data.LBTranRuleHostSection_SectionID);
	});
	//分发规则选择行监听
	table_ind1.uxtable.table.on('row(tranrule_table)', function(obj){
		CHECK_ROW_DATA_RULE = [obj.data];
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	    //取单时间段规则
	    table_ind2.loadData(obj.data.LBTranRule_Id);
	});
	//分发规则双击选择行监听
	table_ind1.uxtable.table.on('rowDouble(tranrule_table)', function(obj){
//		table_ind1.openForm(obj.data.LBTranRule_Id);
	});
	//分发规则按钮事件
	var active = {
		addrlue: function() {//新增关系、
		    table_ind1.openForm('');
		},
		edit: function() {//新增关系、\
			if(CHECK_ROW_DATA_RULE.length==0){
				layer.msg('请选择行',{icon:5});
				return false;
			}
			ChildRuleCheckData();
			var id = CHECK_ROW_DATA_RULE[0].LBTranRule_Id;
		    table_ind1.openForm(id);
		},
		del: function() {//删除
			table_ind1.onDelClick(CHECK_ROW_DATA_RULE[0].LBTranRule_Id,function(){
				if(!CHECK_ROW_DATA[0].LBTranRuleHostSection_SectionID)return false;
				isDel = true;
				table_ind1.loadData(CHECK_ROW_DATA[0].LBTranRuleHostSection_SectionID);
			});
		}
	};
	$('.TranRule .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active[type] ? active[type].call(this) : '';
	});
	//分发规则项目明细按钮事件
	var active1 = {
		additem: function() {//新增项目
		    table_ind2.openWinForm(CHECK_ROW_DATA_RULE[0].LBTranRule_Id,CHECK_ROW_DATA_RULE[0].LBTranRule_CName,CHECK_ROW_DATA[0].LBTranRuleHostSection_SectionID);
		},
		del: function() {//删除项目
//		    table_ind2.openWinForm(CHECK_ROW_DATA_RULE[0].LBTranRule_Id,CHECK_ROW_DATA_RULE[0].LBTranRule_CName,CHECK_ROW_DATA[0].LBTranRuleHostSection_SectionID);
		}
	};
	$('.TranRuleItem .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active1[type] ? active1[type].call(this) : '';
	});
	//新增规则项目按钮是否禁用
	function isBtnEnableRuleItem(bo){
        if(bo){
        	$("#additem").removeClass("layui-btn-disabled").removeAttr('disabled',true);
        }else{
           $("#additem").addClass("layui-btn-disabled").attr('disabled',true);
        }
	}
	//新增规则按钮是否禁用
	function isBtnEnableRule(bo){
        if(bo){
        	$("#addrlue").removeClass("layui-btn-disabled").removeAttr('disabled',true);
            $("#edit").removeClass("layui-btn-disabled").removeAttr('disabled',true);
            $("#del").removeClass("layui-btn-disabled").removeAttr('disabled',true);
        }else{
           $("#addrlue").addClass("layui-btn-disabled").attr('disabled',true);
           $("#edit").addClass("layui-btn-disabled").attr('disabled',true);
           $("#del").addClass("layui-btn-disabled").attr('disabled',true);
        }
	};
	 //分发规则类型数据加载
    function ruletype(callback){
    	var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_RULE_TYPE_URL,
		    async:false,  //使用同步的方式,true为异步方式
			type:'get',
			data:{
				page:1,
				limit:1000,
				where:"lbtranruletype.CName='默认规则'",
				fields:'LBTranRuleType_Id,LBTranRuleType_CName'
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value ||{}).list || [];
				if(list.length==0){
					addruletype(callback);
				}else{
				    DEFAULTDRULETYPEID = list[0].LBTranRuleType_Id;
					callback();
				}
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
    }
  
	//新增分发规则类型（默认)，ID默认为0
    function addruletype(callback){
    	var loadIndex = layer.load();//开启加载层
    	var entity ={CName:'默认规则',IsUse:1};
    	uxutil.server.ajax({
    		async:false,  //使用同步的方式,true为异步方式
            url: ADD_RULE_TYPE_URL,
            type:'POST',
            data: JSON.stringify({entity:entity})
        }, function (data) {
        	layer.close(loadIndex);//关闭加载层
            if (data.success) {
            	var id = data.value.id || "";
            	DEFAULTDRULETYPEID = id;
            	callback();
            } else {
                layer.msg(data.ErrorInfo, { icon: 5});
            }
        });
    }
	//数据传到子窗体
	function ChildRuleCheckData(){
		var obj ={};
		if(CHECK_ROW_DATA_RULE.length>0)obj = CHECK_ROW_DATA_RULE[0];
		return obj;
	}
	function afterTranRuleUpdate(){
		if(!CHECK_ROW_DATA[0].LBTranRuleHostSection_SectionID)return false;
		table_ind1.loadData(CHECK_ROW_DATA[0].LBTranRuleHostSection_SectionID);
	}
	window.afterTranRuleUpdate = afterTranRuleUpdate;
	window.ChildRuleCheckData = ChildRuleCheckData;

});