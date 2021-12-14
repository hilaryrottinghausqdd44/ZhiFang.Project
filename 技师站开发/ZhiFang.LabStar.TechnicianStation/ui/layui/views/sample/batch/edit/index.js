/**
 * 批量修改检验单
 * @author liangyl
 * @version 2021-05-17
 */
var SampleInfoCheckStr = "";
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base',
	uxtable :'ux/table',
	uxbasic: 'views/sample/batch/uxbasic',
    TestFormList: 'views/sample/batch/edit/list', 
	UnTestFormList: 'views/sample/batch/edit/unlist',
	itemtable: 'views/sample/batch/edit/additem',
	delitemtable : 'views/sample/batch/edit/delitem',
	info : 'views/sample/batch/edit/info/info',
	manyresulttable : 'views/sample/batch/edit/manyresult',
	singleresulttable : 'views/sample/batch/edit/singleresult',
	dilutiontable : 'views/sample/batch/edit/dilution',
	resultdeviationtable : 'views/sample/batch/edit/resultdeviation',
	tableSelect: '../src/tableSelect/tableSelect'
}).use(['element', 'uxutil','uxbase','form','TestFormList','UnTestFormList','itemtable','uxbasic','delitemtable','info','manyresulttable','singleresulttable','dilutiontable','resultdeviationtable'], function(){
//}).use(['element','uxutil','form','uxbasic','TestFormList','UnTestFormList'], function(){

var $ = layui.$,
	form = layui.form,
	element = layui.element,
	TestFormList = layui.TestFormList,
	UnTestFormList = layui.UnTestFormList,
	itemtable = layui.itemtable,
	delitemtable = layui.delitemtable,
	uxbasic = layui.uxbasic,
	info = layui.info,
	manyresulttable = layui.manyresulttable,
	singleresulttable = layui.singleresulttable,
	dilutiontable = layui.dilutiontable,
	resultdeviationtable = layui.resultdeviationtable,
	uxbase = layui.uxbase,
	uxutil = layui.uxutil;
		
	//小组ID
	var SECTIONID  = uxutil.params.get(true).SECTIONID;
     //页签是否已加载
    var isTabLoadArr=[];
    //样本单选择列表实例
	var table1_Ind =null;
	//添加检验单列表实例
	var table2_Ind = null;
	//检验单修改实例
	var info_Ind = null;
	//删除项目列表实例
	var table3_Ind = null;
	//多项目结果录入实例
	var table4_Ind = null;
	//单项目结果录入
	var table5_Ind = null;
	//稀释处理
	var table6_Ind = null;
	//结果偏移
	var table7_Ind = null;

	var win = $(window),
		    maxheight = win.height();
	//已选检验单列表实例
	var table0_Ind = TestFormList.render({
		domId: 'TestForm',
		height:'full-35',
		checkClick:function(obj){
			table1_Ind.setStatus(table0_Ind.getListData());
			 //切换到检验单选择页签
            element.tabChange('tabs', 'select'); //切换到：用户管理
		},
		rowDouble:function(obj){
			table0_Ind.del(obj);
			table1_Ind.setStatus(table0_Ind.getListData());
			//切换到检验单选择页签
            element.tabChange('tabs', 'select'); //切换到：用户管理
		},
		done:function(data){
			setformlist(data);
		}
	});
	
	//检验修改渲染
	function form_info(){
		info_Ind = info.render({
			SECTIONID : SECTIONID
		});
		info_Ind.config.FORMLIST = table0_Ind.getListData();
	}
	//检验单选择渲染和实践
	function choice(){
		//样本单选择列表实例
		table1_Ind = UnTestFormList.render({
			domId: 'UnTestForm',
			height:'full-97',
			SECTIONID:SECTIONID,
			SampleInfoCheckStr:SampleInfoCheckStr,
			//删除检验单
			delClick:function(){
				table0_Ind.onDelClick(table0_Ind.getListData(),function(){
					table1_Ind.setStatus([]);
				});
			},
			checkClick:function(obj){
				table0_Ind.add(obj);
				table1_Ind.setStatus(table0_Ind.getListData());
			},
			rowDouble:function(obj){
				table0_Ind.add([obj]);
				table1_Ind.setStatus(table0_Ind.getListData());
			}
		});
	}

	//添加检验项目渲染
	function additem(){
		//样本单选择列表实例
		table2_Ind = itemtable.render({
			elem:'#item_table',
			height:'full-114',
			title:'添加检验项目',
			SECTIONID:SECTIONID,
			size: 'sm',
			FORMLIST:table0_Ind.getListData()
		});
		table2_Ind.instance.reload({data:[]});
		form.render();
	}
	
	//添加删除项目页签渲染
	function delitem(){
        var height = maxheight - $("#del_item_form").height()-95;
		//样本单选择列表实例
		table3_Ind = delitemtable.render({
			elem:'#del_item_table',
			height:height,
			title:'删除检验项目',
			SECTIONID:SECTIONID,
			size: 'sm',
			FORMLIST:table0_Ind.getListData()
		});
		table3_Ind.instance.reload({data:[]});
		form.render();
	}
	
	//多项目结果录入页签渲染
	function manyresult(){
        var height = maxheight - $("#reportvalue_form").height()-95;
		table4_Ind = manyresulttable.render({
			elem:'#item_reportvalue_table',
			height:height,
			title:'多项目结果录入',
			SECTIONID:SECTIONID,
			size: 'sm',
			FORMLIST:table0_Ind.getListData()
		});
		form.render();
	}
	
	//单项目结果录入页签渲染
	function singleresult(){
		table5_Ind = singleresulttable.render({
			elem:'#single_item_reportvalue_table',
			height:'full-115',
			title:'单项目结果录入',
			SECTIONID:SECTIONID,
			size: 'sm',
			FORMLIST:table0_Ind.getListData()
		});
		table5_Ind.instance.reload({data:[]});
		form.render();
	}
	
	//稀释处理页签渲染
	function dilution(){
		table6_Ind = dilutiontable.render({
			elem:'#dilution_table',
			height:'full-94',
			title:'多项目结果录入',
			SECTIONID:SECTIONID,
			size: 'sm',
			FORMLIST:table0_Ind.getListData()
		});
		table6_Ind.instance.reload({data:[]});
		form.render();
	}
	
	//结果偏移签渲染
	function resultdeviation(){
		var height = maxheight - $("#resultdeviation_form").height()-95;
		table7_Ind = resultdeviationtable.render({
			elem:'#resultdeviation_table',
			height:height,
			title:'多项目结果录入',
			SECTIONID:SECTIONID,
			size: 'sm',
			FORMLIST:table0_Ind.getListData()
		});
		table7_Ind.instance.reload({data:[]});
		form.render();
	}
    element.on('tab(tabs)', function(obj){
     	inittab(obj.index);
    });
	//已选检验单数据
	function setformlist(list){
		 //样本单选择列表实例
		 if(table1_Ind)table1_Ind.config.FORMLIST =list;
		//添加检验单列表实例
		 if(table2_Ind)table2_Ind.config.FORMLIST =list;
		//检验单修改实例
		 if(info_Ind)info_Ind.config.FORMLIST =list;
		//删除项目列表实例
		 if(table3_Ind)table3_Ind.config.FORMLIST =list;
		//多项目结果录入实例
		 if(table4_Ind)table4_Ind.config.FORMLIST =list;
		//单项目结果录入
		 if(table5_Ind)table5_Ind.config.FORMLIST =list;
		 //稀释处理
		 if(table6_Ind)table6_Ind.config.FORMLIST =list;
		 //结果偏移
		 if(table7_Ind)table7_Ind.config.FORMLIST =list;
	}
    //渲染    
    function inittab(index){
    	var tab = index+'';
    	var isload=isTabLoadArr.indexOf(index);
    	switch (tab){
    		case '0':
    		    if(isload ==-1){
    		    	choice();//第一次切换页签 -初始化页签
    		    	//样本单列表查询
                    table1_Ind.onSearch();
    		        isTabLoadArr.push(index);
    		    }
    			break;
            case '1'://检验单修
               if(info_Ind)info_Ind.config.FORMLIST = table0_Ind.getListData();
    		    if(isload==-1){
    		    	form_info();//第一次切换页签 -初始化页签
    		        isTabLoadArr.push(index);
    		    }
    			break;
    		case '2'://添加检验项目
    		    if(table2_Ind)table2_Ind.config.FORMLIST = table0_Ind.getListData();
    		    if(isload==-1){
    		    	//第一次切换页签 -初始化页签
    		    	additem();
    		    	isTabLoadArr.push(index);
    		    }
    			break;
    		case '3':  //删除检验项目
    		     if(table3_Ind)table3_Ind.config.FORMLIST = table0_Ind.getListData();
    		    if(isload == -1){
    		    	//第一次切换页签 -初始化页签
    		    	delitem();
    		    	isTabLoadArr.push(index);
    		    }
    			break;
    		case '4':  //多项目结果录入
    		    if(table4_Ind)table4_Ind.config.FORMLIST = table0_Ind.getListData();
    		    if(isload == -1){
    		    	//第一次切换页签 -初始化页签
    		    	manyresult();
    		    	isTabLoadArr.push(index);
    		    }
    		    table4_Ind.loadData();
    			break;
    		case '5':  //单项目结果录入
    		    if(table5_Ind){
    		    	table5_Ind.config.FORMLIST = table0_Ind.getListData();
    		    	if(table5_Ind.config.FORMLIST.length==0)table5_Ind.instance.reload({data:[]});
    		    }
    		    if(isload == -1){
    		    	
    		    	//第一次切换页签 -初始化页签
    		    	singleresult();
    		    	isTabLoadArr.push(index);
    		    }
    			break;	
    		case '6':  //稀释处理
    		    if(table6_Ind)table6_Ind.config.FORMLIST = table0_Ind.getListData();
    		    if(isload == -1){
    		    	//第一次切换页签 -初始化页签
    		    	dilution();
    		    	isTabLoadArr.push(index);
    		    }
    		    table6_Ind.loadData(table0_Ind.getListData());
    			break;	
    		case '7':  //结果偏移
    		    if(table7_Ind)table7_Ind.config.FORMLIST = table0_Ind.getListData();
    		    if(isload == -1){
    		    	//第一次切换页签 -初始化页签
    		    	resultdeviation();
    		    	isTabLoadArr.push(index);
    		    }
    		    table7_Ind.loadData(table0_Ind.getListData());
    			break;		
    	}
    }
	
	//初始化数据
    function init(){
         //默认高度加载
		$(".fiexdHeight").css("height", ($(window).height() - 50) + "px");//设置中间容器高度
		//tab 渲染
		inittab(0);
    }
     //调用init
    $(document).ready(function () {
        var task = setInterval(function () {
            if (SampleInfoCheckStr) {
			    //初始化数据
                init();
                clearInterval(task);
                task = null;
            }
        },100);
    });
});