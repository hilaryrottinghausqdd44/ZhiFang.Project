/**
 * @name：检验单样本单信息
 * @author：liangyl
 * @version 2021-09-07
 */
//var SampleInfoCheckStr = "";
layui.extend({
}).define(['form','uxbase','uxutil','uxtable','uxbasic'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		form = layui.form,
		uxbasic = layui.uxbasic,
		MOD_NAME = 'UnTestFormList';
	
	//获取样本单列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormBySampleNo?isPlanish=true';
	//修改数据服务路径
	var DEIT_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_DeleteBatchLisTestForm';
    //查询是否能删除
    var GET_EDIT_INFO_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormIsDelete';
	var DATA_LIST = [],
	    DATE_WHERE="";
	    //小组ID
	var SECTIONID  = uxutil.params.get(true).SECTIONID;
    //默认查询条件
	var DEFAULTWHERE = 'listestform.MainStatusID=0 and listestform.LBSection.Id='+SECTIONID;
    var sorttype ={
    	GTestDateType :"asc",
    	GSampleNoForOrderType :"asc",
    };
    var isoneopen = true;
	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table" style="overflow-y:hidden;">',
		  '<div class="layui-form" lay-filter="{tableId}-form" id="{tableId}-form">', 
	        '<div class="layui-form-item" style="margin-bottom: 1px;"> ',
	          '<div class="layui-inline" style="margin-right: 0px;">', 
	            '<label class="layui-form-label">检验日期:</label>', 
	            '<div class="layui-input-inline" style=" width: 70px;">', 
	              '<select id="{tableId}-DateType" name="DateType" lay-filter="{tableId}-DateType"> <option value="0">当日</option> <option value="-1">昨日</option> <option value="-1">2天</option> <option value="-2">3天</option> <option value="-4">5天</option> <option value="-6">一周</option> <option value="-9">10天</option> <option value="-14">15天</option> <option value="-19">20天</option> <option value="-29">30天</option> <option value="-44">45天</option> <option value="-59">60天</option> <option value="-74">75天</option> </select>', 
	            '</div>', 
	          '</div>', 
	          '<div class="layui-inline">', 
	            '<input type="text" id="{tableId}-GTestDate" name="{tableId}-GTestDate" class="layui-input myDate" style="width:190px;" placeholder="检验日期" />', 
	            '<i class="layui-icon layui-icon-date"></i>', 
	          '</div>', 
	          '<div class="layui-inline">', 
	            '<label class="layui-form-label">样本号范围:</label>', 
	            '<div class="layui-input-inline" style="width: 60px;">', 
	              '<input type="text" id="{tableId}-sampleno_min" placeholder="" autocomplete="off" class="layui-input" />', 
	            '</div>', 
	            '<div class="layui-form-mid">',
	              '-',
	            '</div>',
	           '<div class="layui-input-inline" style="width: 60px;">', 
	            '<input type="text" id="{tableId}-sampleno_max" placeholder="" autocomplete="off" class="layui-input" />', 
	           '</div>', 
	          '</div>', 
	         '<div class="layui-inline">', 
	           '<div class="layui-btn-group unlist">', 
	            '<button class="layui-btn layui-btn-xs" data-type="search"> <i class="layui-icon"></i>查询 </button>', 
	            '<button class="layui-btn layui-btn-xs"> 已选检验单 </button>', 
	            '<button class="layui-btn layui-btn-xs layui-bg-orange" data-type="del"> <i class="iconfont"></i>&nbsp;删除 </button>', 
	           '</div>', 
	          '</div>', 
	         '</div>', 
	        '</div> ',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>'
	];
	var FORMLIST = [];
	//医嘱单列表
	var UnTestFormList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		
		//对外参数
		config:{
			domId:null,
			height:null,
			DIRECTION:'asc',
			SECTIONID:null,
			FORMLIST :[],
			SampleInfoCheckStr:'',
			//删除检验单
			delClick:function(){},
			//双击
			rowDouble:function(){},
			//选择复选框
			checkClick:function(){}
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			size:'sm',//小尺寸的表格
			where:{},
			height:null,
			page: true,
			limit: 20,
			limits: [20,50,100, 200, 500, 1000, 1500],
			autoSort:false,
			defaultOrderBy:[
				{property:'LisTestForm_GTestDate',direction:'ASC'},
				{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}
			],
//			initSort: { field: 'LisTestForm_GTestDate', type: 'asc' },//type如果大写的话 不能识别
			cols:[[
			    {type: 'checkbox', fixed: 'left'},
				{field:'LisTestForm_Id', width:180, title: '检验单ID', sort: false,hide:true},
				{field:'LisTestForm_GTestDate', width:100, title: '检验日期', sort: true, templet:function(record){
					var value = record["LisTestForm_GTestDate"],
	                    v = uxutil.date.toString(value, true) || '';
	                return v;
				}},
				{field:'LisTestForm_GSampleNoForOrder', width:100, title: '样本号', sort: true, templet:function(record){
					var v = record["LisTestForm_GSampleNo"];
	                return v;
				}},
				{field:'LisTestForm_GSampleNo', width:80, title: '样本号排序',hide:true, templet:function(record){
					var v = record["LisTestForm_GSampleNoForOrder"];
	                return v;
				}},
				{field:'LisTestForm_CName', width:100, title: '姓名' ,sort: false},
				{field:'LisTestForm_LisPatient_GenderName',width:70,title:'性别',sort:false},
				{field:'LisTestForm_GSampleType', width: 100, title: '样本类型', sort: false },
				{field:'LisTestForm_LBSection_CName', width: 150, title: '检验小组', sort: false },
				{field:'LisTestForm_ISource', width: 100, title: '检验单来源', sort: false,hide:true },
				{field:'LisTestForm_LisOrderForm_Id', width: 100, title: '医嘱单ID', sort: false,hide:true },
				{field:'LisTestForm_MainStatusID', width: 100, title: '状态', sort: false,hide:true },
				{field:'LisTestForm_Tab', width: 100, title: '已选择标记', sort: false,hide:false }
		    ]],
			text: {none: '暂无相关数据' },
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,UnTestFormList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,UnTestFormList.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
	
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
		    me.setStatus(me.config.FORMLIST);
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g,me.tableId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);
		//监听日期
		var today = new Date();
		var defaultvalue = uxutil.date.toString(today, true) + " - " + uxutil.date.toString(today, true);
		if(me.config.SampleInfoCheckStr.datewhere){
       	    defaultvalue = me.config.SampleInfoCheckStr.datewhere;
       	    var startDate = defaultvalue.substring(0,10), //开始日期
                endDate = defaultvalue.substring(13,defaultvalue.length); //结束时间
       	    var differenceDate = me.difference(startDate,endDate);
       	    if(differenceDate>0)differenceDate ='-'+differenceDate;
       	    $('#'+me.tableId+'-DateType').val(differenceDate);
        }
		uxbasic.initDate(me.tableId+'-GTestDate',defaultvalue,me.tableId+'-form',true);
        form.render();
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
      	//监听样本单列表排序
        me.uxtable.table.on('sort(' + me.tableId + ')', function (obj) {
            var field = obj.field, //当前排序的字段名
                type = obj.type, //当前排序类型：desc（降序）、asc（升序）、null（空对象，默认排序）
                sort = [];
	        var initSort={ "property": "LisTestForm_GTestDate", "direction": type };
	        if (type == null) {//默认排序
	        	me.tableConfig.initSort = initSort;
	            sort = me.tableConfig.defaultOrderBy;
	         }else{
	         	 var str1 = type.toUpperCase();
	         	 switch (field){
	             	case 'LisTestForm_GTestDate':
	             	    //点击检验日期:
	                    //顺序 检验日期顺序 + 样本号顺序
	                    //逆序 检验日期逆序 + 样本号逆序
	                    sort = [{ "property": "LisTestForm_GTestDate", "direction": str1 }, { "property": "LisTestForm_GSampleNoForOrder", "direction": sorttype.GSampleNoForOrderType }];
	             		sorttype.GTestDateType  = str1;
	             		break;
	                case 'LisTestForm_GSampleNoForOrder':
	                    sort = [{ "property": "LisTestForm_GTestDate", "direction": sorttype.GTestDateType }, { "property": "LisTestForm_GSampleNoForOrder", "direction": str1 }];
	             		sorttype.GSampleNoForOrder  = str1;
	             		break;
	             	default:
	             	  //其他 则直接追加
	                    sort.push({ "property": field, "direction": str1 })
	             		break;
	            }
	        }
	        me.tableConfig.defaultOrderBy = sort;
	        
	        me.onSearch();
        });
        //双击行
		me.uxtable.table.on('rowDouble(' + me.tableId + ')', function(obj){
			//标注选中样式
		    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		    me.config.rowDouble && me.config.rowDouble(obj.data);
		});
		me.uxtable.table.on('checkbox(' + me.tableId + ')', function(obj){
			var checkStatus =  me.uxtable.table.checkStatus(me.tableId);
            var data = checkStatus.data;
            if(obj.checked)me.config.checkClick && me.config.checkClick(data);
		});
        //时间范围计算
	    form.on('select('+me.tableId+'-DateType)', function(data){
	       var today = new Date();
	       var date1 = uxutil.date.toString(uxutil.date.getNextDate(today,data.value), true) + " - " + uxutil.date.toString(today, true);
	       if(DATE_WHERE){
	       	  date1 = DATE_WHERE;
	       }
	       //赋值日期框
	       $("#"+me.tableId+'-GTestDate').val(date1);
	       me.onSearch();
		});
        //按钮事件
		var active = {
			//查询
			search: function() {
				me.onSearch();
			},
			//删除
			del: function() {
				me.config.delClick && me.config.delClick();
			}
		};
		$('.unlist .layui-btn').on('click', function() {
			var type = $(this).data('type');
			active[type] ? active[type].call(this) : '';
		});
	};
	//获取列表数据
	Class.prototype.getListData = function(){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    return list;
	};
	//设置已选标记
	Class.prototype.setStatus = function(FORMLIST){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    	me.config.FORMLIST = FORMLIST ;
	    for(var i=0;i<list.length;i++){
	    	list[i].LAY_CHECKED = false;
	    	var id = list[i].LisTestForm_Id;
	    	list[i].LisTestForm_Tab = '';
	    	for(var j =0;j<me.config.FORMLIST.length;j++){
	    		if(id == me.config.FORMLIST[j].LisTestForm_Id){
	    			list[i].LisTestForm_Tab = '1';
	    			layui.$.extend(me.uxtable.table.cache[me.tableId][i],{'LisTestForm_Tab' : '1'});
//	    			me.config.FORMLIST.splice(j,1); //删除下标为i的元素
	    			break;
	    		}
	    	}
	    }
	    me.cellbgcolor(list);
	};
	//获取按钮查询条件
	Class.prototype.getWhere = function(){
		var me = this,
		    startDate='',endDate='';
   	    var GTestDate = $('#'+me.tableId +'-GTestDate').val();
		if(!GTestDate){
			uxbase.MSG.onWarn("检验日期不能为空!");
            return false;
		}
		var msg = uxbasic.isDateValid({GTestDate:GTestDate});
        if (msg != "") {
			uxbase.MSG.onWarn(msg);
            return false;
        }
    	startDate = GTestDate.substring(0,10); //开始日期
        endDate = GTestDate.substring(13,GTestDate.length); //结束时间
	
		var params = [],
	        where = DEFAULTWHERE;
			//小组Id
		if(me.config.SECTIONID) {
			params.push("listestform.LBSection.Id=" + me.config.SECTIONID + "");
		}
		if(startDate){
			params.push("listestform.GTestDate>='" + startDate + "'");
		}
		if(endDate){
			params.push("listestform.GTestDate<='" + endDate + "'");
		}
		if(params.length > 0) {
			where+= ' and ('+ params.join(' and ')+")";
		}
//		return {"where":where};
		return where;
	};
    //数据查询
	Class.prototype.onSearch = function(){
		var me = this;
        me.getListFromServer(function(list){
        	if(isoneopen){
		    	isoneopen = false;
		        var checklist = me.resultdata(list);
		    	if(checklist.length>0)me.config.checkClick && me.config.checkClick(checklist);
		    }
        	
        	me.uxtable.instance.reload({
				url:'',
				data:list
			});
        });
	};
	//从服务器获取样本单信息列表
	Class.prototype.getListFromServer = function(callback){
		var me = this,
			cols = me.tableConfig.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
	    var beginSampleNo = $('#'+me.tableId +'-sampleno_min').val(),
	       endSampleNo = $('#'+me.tableId +'-sampleno_max').val();
        var url=GET_LIST_URL;
        //样本号范围
		if(beginSampleNo)url+='&beginSampleNo='+beginSampleNo;
		if(endSampleNo)url+='&endSampleNo='+endSampleNo;
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:url,
			type:'get',
			data:{
				page:1,
				limit:100,
				fields:fields.join(','),
				where: me.getWhere(),
				sort:JSON.stringify(me.tableConfig.defaultOrderBy)
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				callback((data.value || {}).list || []);
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		},true);
	};
	
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	 //待选样本单对已选行的检验日期填充背景色
	Class.prototype.cellbgcolor = function(list){
		var me = this;
        list.forEach(function (item, index) {
			if (item.LisTestForm_Tab == '1'){
				//背景色-绿色
				$('div[lay-id="'+me.tableId+'"]').
				find('tr[data-index="' + index + '"]').
				find('td[data-field="LisTestForm_GTestDate"]').
				css('background-color', 'green');
				//字体白色
				$('div[lay-id="'+me.tableId+'"]').
				find('tr[data-index="' + index + '"]').
				find('td[data-field="LisTestForm_GTestDate"]').
				css('color', 'white');
			}else{
	            $(me.tableConfig.elem+'+div .layui-table-body table.layui-table tbody tr[data-index=' + index + '] input[type="checkbox"]').prop('checked', false);
			    $(me.tableConfig.elem+'+div .layui-table-body table.layui-table tbody tr[data-index=' + index + '] input[type="checkbox"]').next().removeClass('layui-form-checked');
				//背景色-绿色
				$('div[lay-id="'+me.tableId+'"]').
				find('tr[data-index="' + index + '"]').
				find('td[data-field="LisTestForm_GTestDate"]').
				css('background-color', 'white');
				//字体白色
				$('div[lay-id="'+me.tableId+'"]').
				find('tr[data-index="' + index + '"]').
				find('td[data-field="LisTestForm_GTestDate"]').
				css('color', 'black');
			}
		});
		form.render('checkbox');
	};
	Class.prototype.resultdata = function(list){
		var me = this,arr=[];
		var checklist = me.config.SampleInfoCheckStr.list;
		for(var i=0;i<list.length;i++){
			var isE=false;
			for(var j=0;j<checklist.length;j++){
				if(list[i].LisTestForm_Id == checklist[j].LisTestForm_Id && list[i].LisTestForm_MainStatusID=='0'){
					isE=true;
					checklist.splice(j, 1); //删除下标为i的元素
					break;
				}
			}
			if(isE)arr.push(list[i]);
		}
		return arr;
	};
	 //时间差
    Class.prototype.difference =  function (start, end) {
        var date1 = uxutil.date.getDate(start);
        var date2 = uxutil.date.getDate(end);
        if (!date1 || !date2) {
            return null;
        }

        var times = date2.getTime() - date1.getTime();
        
        return this.getDateContentByTimes(times);
    };
        //根据时间毫秒数返回时间文字显示内容
    Class.prototype.getDateContentByTimes = function (times){
    	if(!times || isNaN(times)) return null;
    	
    	var result = [];
    	
    	if (times < 0) {
            times = 0 - times;
            result.push('-');
        }
    	
    	//计算出相差天数
        var days = Math.floor(times / (24 * 3600 * 1000));
        if (days > 0) { result.push(days); }
        return result.join('');
    };
	//核心入口
	UnTestFormList.render = function(options){
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
	exports(MOD_NAME,UnTestFormList);
});