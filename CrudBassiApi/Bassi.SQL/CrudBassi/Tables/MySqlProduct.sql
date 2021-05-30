CREATE TABLE `Product` (
 `Id` int(4) NOT NULL AUTO_INCREMENT,
 `IdStatus` int(11) NOT NULL,
 `ITime` datetime NOT NULL,
 `ModifiedDate` datetime NOT NULL,
 `Name` varchar(50),
 PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1
