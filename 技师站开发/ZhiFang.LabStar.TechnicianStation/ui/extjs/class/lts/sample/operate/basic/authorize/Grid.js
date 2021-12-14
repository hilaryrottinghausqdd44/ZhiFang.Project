/**
 * 已有预授权列表
 * @author Jcall
 * @version 2020-09-09
 */
Ext.define('Shell.class.lts.sample.operate.basic.authorize.Grid',{
	extend:'Shell.ux.grid.Panel',
	requires:['Ext.ux.CheckColumn'],
	
	title:'已有预授权列表',
	width:800,
	height:500,
	
	//获取Lis_OperateAuthorize 操作授权服务路径
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateAuthorizeByHQL?isPlanish=true',
	//默认加载
	defaultLoad:false,
	//是否启用序号列
	hasRownumberer:false,
	//是否启用刷新按钮
	hasRefresh:true,
	
	//操作类型
	OperateTypeID:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.onSearch();
	},
	initComponent:function(){
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	//创建数据列
	createGridColumns:function(){		  
		var me = this;
		var columns = [{
			text:'主键ID',dataIndex:'LisOperateAuthorize_Id',width:190,isKey:true,hidden:true,hideable:false
		},{
			text:'授权人',dataIndex:'LisOperateAuthorize_AuthorizeUser',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'授权类型',dataIndex:'LisOperateAuthorize_AuthorizeType',width:60,
			sortable:false,menuDisabled:true,
			renderer: function(value, meta) {
				var v = value || '';
				if(v){
					var info = JShell.System.ClassDict.getClassInfoById('AuthorizeType',v);
					if(info) {
						v = info.Name;
					}
				}
				return v;
			}
		},{
			text:'授权开始时间',dataIndex:'LisOperateAuthorize_BeginTime',width:125,
			sortable:false,menuDisabled:true,//hasTime:true,//isDate:true,hasTime:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
			    var v = me.showDateTime(value,record,meta);
			    if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		        return v;
			}
		},{
			text:'授权结束时间',dataIndex:'LisOperateAuthorize_EndTime',width:125,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
			    var v = me.showDateTime(value,record,meta);
			    if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		        return v;
			}
		},{
			text:'授权操作类型',dataIndex:'LisOperateAuthorize_OperateType',width:80,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'被授权人',dataIndex:'LisOperateAuthorize_OperateUserID',width:80,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'授权说明',dataIndex:'LisOperateAuthorize_AuthorizeInfo',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周一',dataIndex:'LisOperateAuthorize_Day1',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周二',dataIndex:'LisOperateAuthorize_Day2',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周三',dataIndex:'LisOperateAuthorize_Day3',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周四',dataIndex:'LisOperateAuthorize_Day4',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周五',dataIndex:'LisOperateAuthorize_Day5',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周六',dataIndex:'LisOperateAuthorize_Day6',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周日',dataIndex:'LisOperateAuthorize_Day0',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	/**查询数据*/
	onSearch:function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.LabStar','AuthorizeType',function(){
			if(!JShell.System.ClassDict.AuthorizeType){
    			JShell.Msg.error('未获取到授权类型,请重新刷新');
    			return;
    		}
			me.load(null, true, autoSelect);
		});
	},
	showDateTime:function(value,record,meta){
		var me = this;
		var v ="";
		var AuthorizeType = record.get('LisOperateAuthorize_AuthorizeType');
		var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType', '临时');
        if(info.Id == AuthorizeType){
			v = JShell.Date.toString(value, false) || '';
        }else{ 
        	var str = value.split(' ');
        	v = str[1] || '';
        }
		return v;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];
        var sysdate = JShell.System.Date.getDate();
	    var DataAddTime = JShell.Date.toString(sysdate);
	    var v = JShell.Date.getDate(DataAddTime);
	    //周几
        var dayNum = v.getDay();
        var strDay = 'lisoperateauthorize.Day'+[dayNum]+'=1';
        
        
	    if(DataAddTime){
	    	var str = "(lisoperateauthorize.AuthorizeType=1"+
	    	" and lisoperateauthorize.BeginTime<'"+DataAddTime +
	    	"' and lisoperateauthorize.EndTime>='"+DataAddTime +
	    	"' and lisoperateauthorize.IsUse=1"+
	    	" and lisoperateauthorize.OperateTypeID="+me.OperateTypeID+")"+
	    	" or (lisoperateauthorize.AuthorizeType=2"+
            " and lisoperateauthorize.IsUse=1"+
            " and lisoperateauthorize.OperateTypeID="+me.OperateTypeID+")";

//	    	" and lisoperateauthorize.OperateTypeID="+me.OperateTypeID+" and "+strDay+")";
	    	params.push(str);
	    }
	    
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType', '周期');
        var sysdate = JShell.System.Date.getDate();
	    var DataAddTime = JShell.Date.toString(sysdate);
	    var v = JShell.Date.getDate(DataAddTime);
	    
		//按周期类型数据过滤处理,只显示周期打勾的数据，如果开始时间大于结束时间，时间往前挪一天(周期)
		for(var i=0;i<data.list.length;i++){
			//周期类型有效数据
			if(data.list[i].LisOperateAuthorize_AuthorizeType == info.Id){
                var BeginTime = data.list[i].LisOperateAuthorize_BeginTime;
                var EndTime = data.list[i].LisOperateAuthorize_EndTime;
                //当天是周几
		        var dayNum = v.getDay();
		        var str = 'LisOperateAuthorize_Day'+dayNum;
		        //当天数据是否已选
		        var isCheck = data.list[i][str];
		        //开始时间大于结束时间时，需要把上一天的数据也给显示出来
                if(JShell.Date.getDate(BeginTime)>JShell.Date.getDate(EndTime)){
                	//上一天日期
//              	var end = EndTime.split(" ");
//              	var endStr = JShell.Date.toString(sysdate,true) + ' '+end[1];
                	var preday = JShell.Date.getNextDate(JShell.Date.toString(sysdate,true),-1);
                	dayNum = preday.getDay();
		            str = 'LisOperateAuthorize_Day'+dayNum;
		            isCheck = data.list[i][str];
		            if(isCheck=='false')data.list.splice(i, 1);
                }else{
                	if(isCheck=='false')data.list.splice(i, 1);
                }
            }
		}
		return data;
	}
});