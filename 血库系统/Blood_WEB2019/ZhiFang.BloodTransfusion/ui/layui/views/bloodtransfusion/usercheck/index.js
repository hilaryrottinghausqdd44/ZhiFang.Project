layui.extend({
	userform: 'views/bloodtransfusion/usercheck/userform'	
}).use(["form", "userform"], function(){
  "use strict";
  var $ = layui.jquery;
  var form = layui.form;
  var userform = layui.userform;
  
  userform.initUser();
})
