/**
 * 批量样本号更改
 * @author liangyl
 * @version 2021-02-22
 */layui.extend({
	 uxutil: 'ux/util',
	 uxbase: 'ux/base',
	uxbasic: 'views/sample/batch/uxbasic',
	formtable: 'views/sample/batch/editsampleno/list'
 }).use(['table', 'uxutil','uxbase','form','uxbasic','laydate','formtable'], function(){
	var $ = layui.$,
		table = layui.table,
		form = layui.form,
		uxbasic = layui.uxbasic,
		laydate = layui.laydate,
		formtable = layui.formtable,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;
		
	// 样本号批量修改
    var UPDATE_SAMPLENOMODIFY_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormSampleNoModify';
	//小组ID
	var SECTIONID  = uxutil.params.get(true).SECTIONID;

	//列表实例
	var tableInd = formtable.render({
		elem:'#table',
		height:'full-195',
		title:'检验单信息列表',
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
        var msg = isValid();
        if(msg){
			uxbase.MSG.onWarn(msg);
        	return false;
        }
		//列表加载
		tableInd.loadData(getSearchObj());
	});
    //执行
    $('#save').on('click',function(){
    	var data = table.cache['table'];
    	if(!data || data.length==0)	{
			uxbase.MSG.onWarn("没有找到检验单!");
			return;
		}
    	//合并
    	onMerge();
    });
    
    //关闭
    $('#close').on('click',function(){
    	parent.layer.closeAll('iframe');
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
 		},100);
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
			SECTIONID:SECTIONID
		}
	}
	//合并前校验
	function isValid(){
		var  msg = '',str1='',str2='';
		var obj = getSearchObj();
        if(!obj.GTestDate)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验日期不不能为空!';
        //判断日期是否有效
//		if (!(obj.GTestDate instanceof Date))str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确格式的日期!';
        if(!obj.StartSampleNo)str1+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;开始样本号不能为空!';
      
        if(str1)msg+='</br><span style="font-weight:bold;color:blue;">检验单当前样本号的</span>'+str1;
		if(!obj.TGTestDate)str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验日期不不能为空!';
		
		 //判断日期是否有效
//		if (!(obj.TGTestDate instanceof Date))str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请输入正确格式的日期!';
      
        if(!obj.TStartSampleNo)str2+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;开始样本号不能为空!';
        if(str2)msg+='</br><span style="font-weight:bold;color:blue;">检验单目标样本号的</span>'+str2;
        
        if(!obj.GSampleNoForOrder)msg+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验单数量不能为空!';
          //校验数量是否是数字型
		if(!uxbasic.IsNumber(obj.GSampleNoForOrder))msg+='</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;检验单数量输入格式不正确!';
		
        return msg;
	}
    
    //根据当前样本号和目标样本号合并检验单
	function onMerge(){
		var obj = getSearchObj();
		var params ={
			sectionID:SECTIONID,
			curTestDate:obj.GTestDate,
			curMinSampleNo:obj.StartSampleNo,
			sampleCount:obj.GSampleNoForOrder,
			targetTestDate:obj.TGTestDate,
			targetMinSampleNo:obj.TStartSampleNo
		};
		var config = {
			type:'post',
			url:UPDATE_SAMPLENOMODIFY_URL,
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
	//初始化数据
	function init(){
		//日期初始化
        initDateListeners();
	}
 
    //监听新日期控件
    function initDateListeners(){
        var me = this;
        var today = new Date();
		var defaultvalue = uxutil.date.toString(today, true);
		uxbasic.initDate('GTestDate',defaultvalue,'form',false);
		uxbasic.initDate('TGTestDate',defaultvalue,'form',false);
    }
    //初始化
    init();
});