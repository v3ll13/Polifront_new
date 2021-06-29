# POLIFRONT_WEB_API
 
**API Routes**
*	api/auth/
	*	login/
		*	Content-Type : application/json in header
		*	A json as body with keys: *email, password*
*	api/user/
	*	findusers
	*	find/{id}
	*	create
	*	delete/{id}
	*	update
*	api/dossier/
	*	getdossiers
	*	getdossiers/{id}
	*	nouveaudossier
	*	removedraft/{id}
*	api/migrant/
	*	