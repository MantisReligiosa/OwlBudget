module.exports = {
  login: {
    method: 'POST', url: '/api/user/login',
  },
  openProject: {
    method: 'GET', url: '/api/project',
  },
  newProject: {
    method: 'PUT', url: '/api/project/new',
  },
  deleteProject: {
    method: 'DELETE', url: '/api/project',
  },
  checkNewProjectName: {
    method: 'POST', url: '/api/project/checkNewName',
  },
  getProjectsList: {
    method: 'GET', url: '/api/project/catalog',
  },
  getContractTypes: {
    method: 'GET', url: '/api/project/contractTypes',
  },
  getScenarios: {
    method: 'GET', url: '/api/project/scenarios',
  },
  getProjectParams: {
    method: 'GET', url: '/api/project/params',
  },
  patchProjectName: {
    method: 'PATCH', url: '/api/project/params/name',
  },
  patchProjectLocation: {
    method: 'PATCH', url: '/api/project/params/location',
  },
  patchProjectRegion: {
    method: 'PATCH', url: '/api/project/params/region',
  },
  patchProjectDescription: {
    method: 'PATCH', url: '/api/project/params/description',
  },
  getProjectObjects: {
    method: 'GET', url: '/api/project/objects',
  },
  newLot: {
    method: 'POST', url: '/api/project/objects/lot',
  },
  deleteLot: {
    method: 'DELETE', url: '/api/project/objects/lot',
  },
  patchLotName: {
    method: 'PATCH', url: '/api/project/objects/lot/name',
  },
  newCluster: {
    method: 'POST', url: '/api/project/objects/cluster',
  },
  deleteCluster: {
    method: 'DELETE', url: '/api/project/objects/cluster',
  },
  patchClusterName: {
    method: 'PATCH', url: '/api/project/objects/cluster/name',
  },
  patchClusterWellType: {
    method: 'PATCH', url: '/api/project/objects/cluster/wellType',
  },
  newWell: {
    method: 'POST', url: '/api/project/objects/well',
  },
  getWellHeaders: {
    method: 'GET', url: '/api/project/objects/well/headers',
  },
  patchWellName: {
    method: 'PATCH', url: '/api/project/objects/well/name',
  },
  patchWellConstruction: {
    method: 'PATCH', url: '/api/project/objects/well/construction',
  },
  patchWellDrillingRig: {
    method: 'PATCH', url: '/api/project/objects/well/drillingRig',
  },
  deleteWell: {
    method: 'DELETE', url: '/api/project/objects/well',
  },
  getProjectDrillingRigSequences: {
    method: 'GET', url: '/api/project/drillingRigSequences',
  },
  moveToSequence: {
    method: 'PATCH', url: '/api/project/drillingRigSequences/moveToSequence',
  },
  moveToNewSequence: {
    method: 'POST', url: '/api/project/drillingRigSequences/moveToNewSequence',
  },
  patchSequenceName: {
    method: 'PATCH', url: '/api/project/drillingRigSequences/name',
  },
  getStages: {
    method: 'GET', url: '/api/project/constructionSchedule',
  },
  newStage: {
    method: 'POST', url: '/api/project/constructionSchedule',
  },
  deleteStage: {
    method: 'DELETE', url: '/api/project/constructionSchedule',
  },
  editStage: {
    method: 'PATCH', url: '/api/project/constructionSchedule',
  },

  /*
     * Справочники:
     */
  getRegionCatalog: {
    method: 'GET', url: '/api/region/catalog',
  },
  getRegionCatalogItem: {
    method: 'GET', url: '/api/region/catalogItem',
  },
  geWellTypeCatalog: {
    method: 'GET', url: '/api/wellType/catalog',
  },
  getWellTypeCatalogItem: {
    method: 'GET', url: '/api/wellType/catalogItem',
  },
  getWellConstructionCatalog: {
    method: 'GET', url: '/api/wellConstruction/catalog',
  },
  getWellConstructionCatalogItem: {
    method: 'GET', url: '/api/wellConstruction/catalogItem',
  },
  getDrillingRigCatalog: {
    method: 'GET', url: '/api/drillingRig/catalog',
  },
  getDrillingRigCatalogItem: {
    method: 'GET', url: '/api/drillingRig/catalogItem',
  },
};
