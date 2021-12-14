$(function () {
    //返回按钮监听
    $(".page-top-back").on("click", function () {
        history.go(-1);
    });
    
//  //医生信息服务
//  var DOCTOR_INFO_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppDoctService.svc/WXAS_BA_GetDoctorInformation";
//  var DOCTOR_INFO
//  
//  //获取医生户信息
//  function getDoctorInfo(){
//  	var url = DOCTOR_INFO_URL;
//		JcallShell.Server.ajax({
//			url:url
//		},function(data){
//			if(data.success){
//				MyInfo = data.value;
//				initMyInfo();
//			}else{
//				
//			}
//		});
//  }
});