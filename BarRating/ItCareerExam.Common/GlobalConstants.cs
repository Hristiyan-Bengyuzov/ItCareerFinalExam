namespace ItCareerExam.Common
{
	public static class GlobalConstants
	{
		// bar constants
		public const int BarNameMaxLength = 64;
		public const int BarDescriptionMaxLength = 255;
		// 2mb in bytes
		public const long BarPhotoMaxSize = 2097152;

        // review constants
        public const int ReviewTextMaxLength = 150;
		public const int ReviewScoreMin = 1;
		public const int ReviewScoreMax = 10;

		// role constants
        public const string AdministratorRole = "Admin";
		public const string UserRole = "User";
	}
}
