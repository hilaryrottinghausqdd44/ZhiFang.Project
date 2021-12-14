/**
 * 项目详细信息显示、所属医嘱项目信息
 * @author liangyl
 * @version 2021-05-28
 */
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base',
	uxtable:'ux/table',
	itemgrouptable: 'views/sample/tab/sample/dlinfo/itemgroup',
	rangetable: 'views/sample/tab/sample/dlinfo/range',
	rangeexptable : 'views/sample/tab/sample/dlinfo/rangeexp',
	calctable : 'views/sample/tab/sample/dlinfo/calc',
	expform : 'views/sample/tab/sample/dlinfo/expform',	
	infoform : 'views/sample/tab/sample/dlinfo/info',
}).use(['uxutil','uxbase','form','element','itemgrouptable','rangetable','rangeexptable','calctable','expform','infoform'], function(){
	var $ = layui.$,
		form = layui.form,
		element = layui.element,
		itemgrouptable = layui.itemgrouptable,
		rangetable = layui.rangetable,
		rangeexptable =	 layui.rangeexptable,
		calctable = layui.calctable,
		expform = layui.expform,
		infoform = layui.infoform,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;

    //获取项目服务路径
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemByHQL?isPlanish=true';
    //专业
	var GET_SPECIALTY_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSpecialtyByHQL?isPlanish=true';
	//项目id、
	var ID = uxutil.params.get(true).ID;
	//组合项目id、
	var PID = uxutil.params.get(true).PID;
	//检验单id
	var TESTFORMID = uxutil.params.get(true).TESTFORMID;
	//组合项目0=单项  1=组合 2=组套
	var GROUPTYPE ='0';
	//项目详细信息
	var ITEM_DATA ={};
    //组合项目详细信息
	var GROUP_ITEM_DATA ={};
	//组合项目是否已切换
	var isGroupItemLoad = false;
    //初始化
	function init(){
		//项目初始化
	    var info_ind = infoform.render({});
	    //单个项目信息
	    info_ind.loadData(ID,function(list){
	    	ITEM_DATA =list[0];
			if(list.length==0) {
				element.tabDelete('infotab', 'ItemRangeExp'); //删除：
			}else{
				
			   //详细信息li内容 修改
			   $('#info1').text(ITEM_DATA.LBItem_CName+'('+ITEM_DATA.LBItem_SName+')详细信息');
			    //计算项目处理
			    initcalc();
				 //生成详细信息组件
				info_ind.createCom(list,function(html){
					$('#infoform').html(html);
				});
			}
		});
	    if(!PID){
	  	   //删除指定Tab项
	       element.tabDelete('tabs', 'group_item'); //删除：
	    }else{
	    	//组合项目、
		    info_ind.loadData(PID,function(list){
		    	GROUP_ITEM_DATA = list[0];
		    	GROUPTYPE = '1';
		   	    //组合项目li内容 修改
		        $('#group_item1').text(GROUP_ITEM_DATA.LBItem_CName+'组合');
			});
	    }
	}
     //初始化计算项目
     function initcalc(){
     	var IsCalcItem = ITEM_DATA.LBItem_IsCalcItem+'';
	    //是否存在计算项目
	    if(IsCalcItem=='true'){
	    	//初始化计算项目
		    var table_calc_ind = calctable.render({
				elem:'#calc_table',
				height:'full-285',
				title:'计算项目',
				size: 'sm'
			});
			table_calc_ind.loadData(ID);
		}else{
			element.tabDelete('infotab', 'Calc'); //删除：
		}
     }
	//创建组合列表
	function createGroupTable(){
		//组合项目初始化
	    var info_ind2 = infoform.render({});
		 //生成详细信息组件
		info_ind2.createCom([GROUP_ITEM_DATA],function(html){
			$('#groupinfoform').html(html);
		});
		
		var table_group_ind = itemgrouptable.render({
			elem:'#group_table',
			height:'full-250',
			title:'组合列表',
			size: 'sm',
			TESTFORMID:TESTFORMID,
			ID:PID
		});
		table_group_ind.loadData();
	}
    //项目详细信息与组合项目信息页签切换
    element.on('tab(tabs)', function(obj){
     	if(obj.index ==1 && !isGroupItemLoad){//切换到组合项目
		   createGroupTable();
           isGroupItemLoad = true;
     	}
    });
    //参考值范围初始化
    var table_range_ind = rangetable.render({
		elem:'#range_table',
		height:'full-285',
		title:'参考值范围',
		size: 'sm'
	});
	table_range_ind.loadData(ID);
    
    //参考值范围后处理初始化
    var exp_ind = expform.render({});
    exp_ind.loadData(ID,function(list){
		//没有参考范围后处理数据 不显示页签
		if(list.length==0) {
			element.tabDelete('infotab', 'ItemRangeExp'); //删除：
		}else{
			exp_ind.createCom(list);
		}
	});
    //描述结果判断初始化
    var table_rangeexp1_ind = rangeexptable.render({
		elem:'#rangeexp1_table',
		height:'full-285',
		title:'描述结果判断',
		size: 'sm'
	});
	var where1 = 'lbitemrangeexp.LBItem.Id='+ID+ ' and lbitemrangeexp.JudgeType=1';
	table_rangeexp1_ind.loadData(where1,function(list){
		//没有描述结果判断初始化数据 不显示页签
		if(list.length==0) {
			element.tabDelete('infotab', 'ItemRangeExp1'); //删除：
		}else{
			table_rangeexp1_ind.instance.reload({data:list});
		}
	});

    //初始化
    init();
});