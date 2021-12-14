/**
 * 报告列表
 * @author Jcall
 * @version 2016-12-12
 */
Ext.define('Shell.ReportSearchDoctor.class.List',{
    extend: 'Shell.ReportPrint.class.PrintList',
	requires: [
		'Shell.ux.form.field.DateArea'
    ],
    /**获取数据服务路径*/
	selectUrl: '/ServiceWCF/ReportFormService.svc/SelectReport?fields=ReportFormID,FormNo,' +
		'SAMPLENO,SECTIONNO,CNAME,CLIENTNO,SectionType,RECEIVEDATE,PRINTTIMES,ItemName,PATNO,Serialno',
		
    /**默认加载数据*/
	defaultLoad:true,
    /**默认顺序*/
    defaultOrderBy:[],
    /**默认报告天数*/
	defaultDates: 1,
    /**是否开启打印功能*/
	hasPrint: false,
	
	/**医生编号映射字段*/
    DoctorNoField:null,
    /**报告日期字段*/
	ReportDateField:null,
    
	initComponent:function(){
		var me = this;
		
		me.selectUrl += ',' + me.DoctorNoField + ',' + me.ReportDateField;
		
		var end = new Date();
		var start = Shell.util.Date.getNextDate(end, 1 - me.defaultDates);
		
		//按钮栏
		me.toolbars = [{
			dock:'top',itemId:'toptoolbar',
			buttons:['refresh','-',{
				xtype:'uxdatearea',itemId:'date',name:'date',
				fieldLabel:'报告日期',value:{start:start,end:end},
				listeners:{
					enter:function(field,dateField){me.onSearch();}
				}
			}]
		},{
			dock:'top',itemId:'toptoolbar2',
			buttons:[{
				itemId:'doctor',emptyText:'医生编号',btype:'searchtext',
				labelAlign:'right',fieldLabel:'医生',labelWidth:40,width:150
			},{
				itemId:'patno',emptyText:'病历号',btype:'searchtext',
				labelAlign:'right',fieldLabel:'患者',labelWidth:40,width:150
			},{
				btype:'searchtext',hidden:true,width:0
			}]
		}];
		
		//数据列
		me.columns = [
			{ xtype: 'rownumberer', text: '序号', width: 50, align: 'center' },
		    {
		        dataIndex:me.ReportDateField, text: '报告日期', width: 100, sortable: false, 
		        renderer: function (v, meta, record) {
//		        	var value = '';
//		        	//当这个标本 小组号SectionNo=（2-14或41或43）时，取一审日期CheckDate，否则都取二审日期SenderTime2
//		        	var SectionNo = record.get('SECTIONNO');
//		        	if(SectionNo>=2 && SectionNo<=14 || SectionNo==41 || SectionNo==43){
//		        		value = record.get('CHECKDATE');
//		        	}else{
//		        		value = record.get('SENDERTIME2');
//		        	}
//		            value = v ? Shell.util.Date.toString(value, true) : "";
//		            return value;
		            
		            var value = v ? Shell.util.Date.toString(v, true) : "";
		            return value;
		        }
		    },
		    { dataIndex: 'CNAME', text: '姓名', width: 60, sortable: false },
		    {
		        dataIndex: 'ItemName', text: '检验项目', width: 100, sortable: false,
		        renderer: function (value, meta, record) {
		            if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
		            return value;
		        }
		    },
		    { dataIndex: 'SAMPLENO', text: '样本号', width: 100, sortable: false },
		    
		    { dataIndex: 'ReportFormID', text: '主键', hideable: false, hidden: true, type: 'key' },
		    { dataIndex: 'SECTIONNO', text: '检验小组编号', hideable: false, hidden: true },
		    { dataIndex: 'SectionType', text: '小组类型', hideable: false, hidden: true },
		    { dataIndex: 'CHECKDATE', text: '一审日期', hideable: false, hidden: true },
		    { dataIndex: 'SENDERTIME2', text: '二审日期', hideable: false, hidden: true }
		];
		
		me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl,
			toptoolbar = me.getComponent('toptoolbar'),
			toptoolbar2 = me.getComponent('toptoolbar2'),
			date = toptoolbar.getComponent('date').getValue(),
			doctor = toptoolbar2.getComponent('doctor').getValue(),
			patno = toptoolbar2.getComponent('patno').getValue(),
			where = [];
			
		//标本前的日期，以报告发出日期为主，但必须注意的是：
		//当这个标本 小组号SectionNo=（2-14或41或43）时，取一审日期CheckDate，否则都取二审日期SenderTime2
		//((SectionNo>=2 and SectionNo<=14) or SectionNo=41 or SectionNo=43) and CheckDate>=2016-01-01 and CheckDate<2016-01-02
		//((SectionNo<2 or SectionNo>14) and SectionNo<>41 and SectionNo<>43) and SenderTime2>=2016-01-01 and SenderTime2<2016-01-02
		
		if(date){
//			var checkDateWhere = ['((SectionNo>=2 and SectionNo<=14) or SectionNo=41 or SectionNo=43)'],
//				senderTime2Where = ['((SectionNo<2 or SectionNo>14) and SectionNo<>41 and SectionNo<>43)'];
//				
//			if(date.start){
//				checkDateWhere.push("CheckDate>='" + Shell.util.Date.toString(date.start,true) + "'");
//				senderTime2Where.push("SenderTime2>='" + Shell.util.Date.toString(date.start,true) + "'");
//			}
//			if(date.end){
//				checkDateWhere.push("CheckDate<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(date.end),true) + "'");
//				senderTime2Where.push("SenderTime2<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(date.end),true) + "'");
//			}
//			
//			var dateWhere = '';
//			if(checkDateWhere.length > 0){
//				dateWhere = '((' + checkDateWhere.join(" and ") + ') or (' + senderTime2Where.join(" and ") + '))';
//			}
//			
//			where.push(dateWhere);
			
			var dateWhere = [];
			if(date.start){
				dateWhere.push(me.ReportDateField + ">='" + Shell.util.Date.toString(date.start,true) + "'");
			}
			if(date.end){
				dateWhere.push(me.ReportDateField + "<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(date.end),true) + "'");
			}
			where.push(dateWhere.join(" and "));
		}
		
		//医生编号
		if(doctor){
			where.push(me.DoctorNoField + "='" + doctor + "'");
		}
		//病历号
		if(patno){
			where.push("PatNo='" + patno + "'");
		}
		
		//默认条件
		if(me.defaultWhere && me.defaultWhere != ''){
			where.push(me.defaultWhere);
		}
		
		if(where.length > 0){
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'where=' + Shell.util.String.encode(where.join(" and "));
		}
		
		//默认排序
		if (me.defaultOrderBy.length > 0) {
	        var len = me.defaultOrderBy.length;
                orderby = [];

            for (var i = 0; i < len;i++){
                orderby.push(me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order);
	        }

	        var index = url.indexOf('where=');

	        if (index != -1) {
	            url += ' ORDER BY ' + orderby.join(",").replace('\'','%27');
	        }
	    }
		
		return url;
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this;
	    var fields = [
			'ReportFormID','FormNo','SAMPLENO','SECTIONNO','CNAME','CLIENTNO','SectionType',
			'ItemName', 'PATNO', 'Serialno', 'PageName', 'PageCount'
	    ];
	    fields.push(me.DoctorNoField,me.ReportDateField);
		
	    return fields;
	}
});