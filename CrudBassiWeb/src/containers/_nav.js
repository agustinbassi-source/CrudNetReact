export default [
  {
    _tag: 'CSidebarNavDropdown',
    name: 'Receipts',
    route: '/receipts',
    icon: 'cil-puzzle',
    _children: [
      {
        _tag: 'CSidebarNavItem',
        name: 'List',
        to: '/Receipts',
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Create',
        to: '/Receipts/Create',
      },
    ]
  },
  {
    _tag: 'CSidebarNavDropdown',
    name: 'Clients',
    route: '/clients',
    icon: 'cil-puzzle',
    _children: [
      {
        _tag: 'CSidebarNavItem',
        name: 'List',
        to: '/Clients',
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Create',
        to: '/Clients/Create',
      },
    ]
  },
  {
    _tag: 'CSidebarNavDropdown',
    name: 'Products',
    route: '/products',
    icon: 'cil-puzzle',
    _children: [
      {
        _tag: 'CSidebarNavItem',
        name: 'List',
        to: '/Products',
      },
      {
        _tag: 'CSidebarNavItem',
        name: 'Create',
        to: '/Products/Create',
      },
    ]
  },
]
