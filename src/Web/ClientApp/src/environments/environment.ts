// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  ahsPwoAPI: {
    url: 'http://localhost:5000/api',
    endPoints: {
      projects: '/projects',
      projectsBudget: '/projectsbudget',
      projectsMedia: '/projectsmedia',
      projectsMetadata: '/projectsmetadata',
      projectsGeneral: '/projectsgeneral',
      projectsStatusfilter: '/projectstatusfilter',
      projectfile: '/projectfile',
      projectsheet: '/projectsheet',
      financingforms: '/financingforms',
      budget: '/budgets',
      participants: '/participants',
      participantsFilter: '/participantsfilter',
      projectstatusFilter: '/projectstatusfilter',
      links: '/links',
      profiles: '/profiles',
      states: '/states',
      tasks: '/tasks',
      settings: '/settings'
    }
  }
};


