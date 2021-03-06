if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[VIEW_RoleModules_RoleName]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[VIEW_RoleModules_RoleName]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW dbo.VIEW_RoleModules_RoleName
AS
SELECT dbo.HR_Departments.ID, dbo.RBAC_RoleModules.*, 
      (CASE WHEN DeptID = 0 THEN
          (SELECT TOP 1 OrgCName
         FROM RM_DOMAIN) ELSE dbo.HR_Departments.CName END) AS DeptCName, 
      dbo.HR_Posts.CName AS PostCName, dbo.HR_Positions.CName AS PositionCName, 
      dbo.HR_Employees.NameL + dbo.HR_Employees.NameF AS EmplCName
FROM dbo.HR_Positions RIGHT OUTER JOIN
      dbo.RBAC_RoleModules LEFT OUTER JOIN
      dbo.HR_Posts ON 
      dbo.RBAC_RoleModules.PostID = dbo.HR_Posts.ID LEFT OUTER JOIN
      dbo.HR_Employees ON dbo.RBAC_RoleModules.EmplID = dbo.HR_Employees.ID ON 
      dbo.HR_Positions.ID = dbo.RBAC_RoleModules.PositionID LEFT OUTER JOIN
      dbo.HR_Departments ON dbo.RBAC_RoleModules.DeptID = dbo.HR_Departments.ID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

