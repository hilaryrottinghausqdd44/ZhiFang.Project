/**
 * 批量样本错位处理
 * @author liangyl
 * @version 2021-05-07
 */layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base',
	uxtable: 'ux/table',
	uxbasic: 'views/sample/batch/uxbasic',
	formtable: 'views/sample/batch/dislocation/list',
	lefttable:'views/sample/batch/dislocation/leftlist',	
	righttable:'views/sample/batch/dislocation/rightlist',
	tableSelect: '../src/tableSelect/tableSelect'
 }).use(['uxutil','uxbase','form','uxbasic','laydate','formtable','lefttable','righttable','tableSelect'], function(){
	var $ = layui.$,
		form = layui.form,
		uxbasic = layui.uxbasic,
		laydate = layui.laydate,
		formtable = layui.formtable,
		lefttable = layui.lefttable,
		righttable = layui.righttable,
		tableSelect = layui.tableSelect,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;
		
	//获取仪器数据	
	var GET_EQUIP_LIST_URL = uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true';
	//批量提取仪器样本单结果
    var SAVE_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_BatchExtractEquipResult';

	//小组ID
	var SECTIONID  = uxutil.params.get(true).SECTIONID;

	var DEFAULT_DATA = {},
	    AFTER_SAVE = function(){};//保存后回调函数

    //检验单仪器样本单列表
	var table0_Ind = formtable.render({
		elem:'#table',
		height:'full-365',
		title:'检验单,仪器样本单列表',
		size: 'sm',
		done:function(res,curr,count){
			setTimeout(function(){
				var tr = table0_Ind.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}
			},0);
		}
	});
	table0_Ind.table.on('row(table)', function(obj){
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        table1_Ind.loadData(obj.data.LisTestForm_Id);
	    table2_Ind.loadData(obj.data.LisTestForm_EquipFormID);
	});

    //当前检验单检验结果
	var table1_Ind = lefttable.render({
		elem:'#left_table',
		height:'full-390',
		title:'当前检验单检验结果',
		size: 'sm'
	});
	
	//仪器项目结果列表
	var table2_Ind = righttable.render({
		elem:'#right_table',
		height:'full-390',
		title:'仪器项目结果列表',
		size: 'sm'
	});

	
	//确认查询功能
	$('#search').on('click',function(){
		var DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/; //判断是否是日期格式 yyyy-mm-dd
        if (!uxutil.date.isValid($('#GTestDate').val()) || !DATE_FORMAT.test($('#TGTestDate').val())) {
			uxbase.MSG.onWarn("检验单当前样本号的日期格式不正确!");
            return false;
        }
        if (!uxutil.date.isValid($('#TGTestDate').val()) || !DATE_FORMAT.test($('#TGTestDate').val())) {
			uxbase.MSG.onWarn("检验单目标样本号的日期格式不正确!");
            return false;
        }
		var msg = isValidMerge();
		if(msg){
			uxbase.MSG.onWarn(msg);
			return false;
		}else{
			table0_Ind.loadData(getSearchObj());
		}
	});
    
    //执行
    $('#save').on('click',function(){
    	var list = table0_Ind.table.cache.table;
    	if(!list || list.length==0)	{
			uxbase.MSG.onWarn("没有找到检验单!");
			return;
		}
    	//批量重新提取仪器结果
    	onSaveClick();
    });
	//开始样本号改变 --
	$("#StartSampleNo").on('input propertychange',function(){
 	   //校验数量是否是数字型
		if(!uxbasic.IsNumber($("#GSampleNoForOrder").val()) && $("#GSampleNoForOrder").val()) {
			$("#TEndSampleNo").html('');
			$("#EndSampleNo").html('');
			uxbase.MSG.onWarn("请输入正确格式的检验单数量!");
			return false;
		}
		
 		setTimeout(function(){
			$("#EndSampleNo").html('');
			if($("#GSampleNoForOrder").val() && $("#StartSampleNo").val()){
				//查询样本截止样本单号
			    uxbasic.endSampleNo($("#StartSampleNo").val(),$("#GSampleNoForOrder").val(),function(data){
			    	$('#EndSampleNo').html(data);
			    });
			}
 		},200);
	});
    //样本号改变--目标
	$("#TStartSampleNo").on('input propertychange',function(){	
		//校验数量是否是数字型
		if(!uxbasic.IsNumber($("#GSampleNoForOrder").val()) && $("#GSampleNoForOrder").val()) {
			$("#TEndSampleNo").html('');
			$("#EndSampleNo").html('');
			uxbase.MSG.onWarn("请输入正确格式的检验单数量!");
			return false;
		}
		
        setTimeout(function(){
			$("#TEndSampleNo").html('');
			if($("#GSampleNoForOrder").val() && $("#TStartSampleNo").val()){
				//查询目标样本截止样本单号
			    uxbasic.endSampleNo($("#TStartSampleNo").val(),$("#GSampleNoForOrder").val(),function(data){
			    	$('#TEndSampleNo').html(data);
			    });
			}
		},100);
	});
	//数量改变
	$("#GSampleNoForOrder").on('input propertychange',function(){
		//校验数量是否是数字型
		if(!uxbasic.IsNumber($("#GSampleNoForOrder").val())) {
			$("#TEndSampleNo").html('');
			$("#EndSampleNo").html('');
			uxbase.MSG.onWarn("请输入正确格式的检验单数量!");
			return false;
		}
		
		setTimeout(function(){
			$("#TEndSampleNo").html('');
			$("#EndSampleNo").html('');
			
			if($("#StartSampleNo").val() && $("#GSampleNoForOrder").val()){
				//查询样本截止样本单号
			    uxbasic.endSampleNo($("#StartSampleNo").val(),$("#GSampleNoForOrder").val(),function(data){
			    	$('#EndSampleNo').html(data);
			    });
			}
			if($("#TStartSampleNo").val() && $("#GSampleNoForOrder").val()){
				//查询目标样本截止样本单号
			    uxbasic.endSampleNo($("#TStartSampleNo").val(),$("#GSampleNoForOrder").val(),function(data){
			    	$('#TEndSampleNo').html(data);
			    });
			}
		},100);
	});
     //icon 前存在icon 则点击该icon 等同于点击input
    $("input.layui-input+.layui-icon").on('click', function () {
        if (!$(this).hasClass("myDate")) {
            $(this).prev('input.layui-input')[0].click();
            return false;//不加的话 不能弹出
        }
    });
	 
	//获取查询参数
	function getSearchObj(){
		var GTestDate = document.getElementById('GTestDate').value,
		    StartSampleNo = document.getElementById('StartSampleNo').value,
		    GSampleNoForOrder = document.getElementById('GSampleNoForOrder').value,
		    EndSampleNo = document.getElementById('EndSampleNo').innerText,
		    TGTestDate = document.getElementById('TGTestDate').value,
		    TStartSampleNo =  document.getElementById('TStartSampleNo').value,
		    TEndSampleNo = document.getElementById('TEndSampleNo').innerText;
		return {
			GTestDate : GTestDate,//uxutil.Date.toString(GTestDate,true),
			StartSampleNo : StartSampleNo,
			GSampleNoForOrder : GSampleNoForOrder,
			TGTestDate : TGTestDate,//uxutil.Date.toString(TGTestDate,true),
			TStartSampleNo : TStartSampleNo,
			TEndSampleNo : TEndSampleNo,
			EndSampleNo : EndSampleNo,
			SECTIONID:SECTIONID,
			EquipID:$("#EquipID option:checked").text()
		}
	}

	 //获取检验单id和仪器检验单id字符列表
    function getIds(){
    	var list = table0_Ind.table.cache.table;
        var testFormIDList=[],equipFormIDList=[];
    	for(var i =0;i<list.length;i++){
    		if(list[i].LisTestForm_IsExec=='true'){
    			testFormIDList.push(list[i].LisTestForm_Id);
    		    equipFormIDList.push(list[i].LisTestForm_EquipFormID);
    		}
    	}
    	return {
    		testFormIDList:testFormIDList.join(','),
    		equipFormIDList:equipFormIDList.join(',')
    	};
    }
	
    //批量提取仪器结果
	function onSaveClick(){
		var obj = getSearchObj();
		
		var isChangeSampleNo = $("input[name='isChangeSampleNo']").val(),//是否改变样本单样本号
			isDelAuotAddItem = $("input[name='isDelAuotAddItem']").val()//是否删除检验单中仪器自增项目
		var idsobj =getIds();
	
	    //testFormIDList, equipFormIDList，两个参数的ID要对应，就是说顺序一致
		var params ={
			testFormIDList:getIds().testFormIDList,//检验单id字符串列表
			equipFormIDList:getIds().equipFormIDList,//仪器检验单id字符串列表
			isChangeSampleNo:isChangeSampleNo ? true : false,//是否改变样本单样本号
			isDelAuotAddItem:isDelAuotAddItem ? true : false//是否删除检验单中仪器自增项目
		};

		var config = {
			type:'post',
			url:SAVE_URL,
			data:JSON.stringify(params)
		};
       
		uxutil.server.ajax(config,function(data){
			if(data.success){
				uxbase.MSG.onSuccess("保存成功!");
//				AFTER_SAVE(data.value);
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	}

    //初始化仪器
    function selectEquipList(CNameElemID, IdElemID) {
        var me = this;
        var CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null;
        var fields = ['Id','CName','Shortcode'],
			url = GET_EQUIP_LIST_URL + '&where=lbequip.LBSection.Id='+SECTIONID +' and lbequip.IsUse=1';
		url += '&fields=LBEquip_' + fields.join(',LBEquip_');
        if(!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBEquip_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbequip.CName,lbequip.Shortcode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '仪器名称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'LBEquip_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBEquip_CName', width: 200, title: '项目名称', sort: false },
                    { field: 'LBEquip_Shortcode', width: 120, title: '快捷码', sort: false }
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
                    if(data.list && data.list.length>0){//默认选择第一个仪器
                    	$("#EquipID").val(data.list[0].LBEquip_Id);
                        $("#EquipName").val(data.list[0].LBEquip_CName);
                    }
                    
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
                    $(elem).val(record["LBEquip_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["LBEquip_Id"]);
                }else{
                	 $(elem).val("");
                    if (IdElemID) $("#" + IdElemID).val("");
                }
            }
        });
       $("#EquipName+i.layui-icon").click();
       tableSelect.hide();
    }
	
	//初始化系统下拉框
	function initSystemSelect(){
        selectEquipList('EquipName','EquipID');
	}

	function init(){
		//初始化下拉框
		initSystemSelect();
		//日期初始化
        initDateListeners();
	    //日期默认时间
	    var today = new Date();
	    $('#GTestDate').val(uxutil.date.toString(today, true));
	    $('#TGTestDate').val(uxutil.date.toString(today, true));
		
		table0_Ind.instance.reload({data:[]});	
		table1_Ind.instance.reload({data:[]});	
		table2_Ind.instance.reload({data:[]});
	}
	//初始化
    init();
	 //监听新日期控件
    function initDateListeners(){
        var me = this;
        //监听日期
		var today = new Date();
		var defaultvalue = uxutil.date.toString(today, true);
		uxbasic.initDate('GTestDate',defaultvalue,'form',false);
		uxbasic.initDate('TGTestDate',defaultvalue,'form',false);
    }
    function isValidMerge(){
		var msg = '',str1='',str2='';
		var obj = getSearchObj();
        if(!obj.GTestDate)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验日期不不能为空!';
        //判断日期是否有效
//		if (!(obj.GTestDate instanceof Date))str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确格式的日期!';
        if(!obj.StartSampleNo)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;开始样本号不能为空!';
        if(!obj.GSampleNoForOrder)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验单数量不能为空!';
        if(str1)msg+='</br><span style="font-weight:bold;color:blue;">检验单当前样本号的</span>'+str1;
		if(!obj.TGTestDate)str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验日期不不能为空!';
		 //判断日期是否有效
//		if (!(obj.TGTestDate instanceof Date))str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确格式的日期!';
      
        if(!obj.TStartSampleNo)str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;开始样本号不能为空!';
        if(str2)msg+='</br><span style="font-weight:bold;color:blue;">检验单目标样本号的</span>'+str2;
        return msg;
	}
});