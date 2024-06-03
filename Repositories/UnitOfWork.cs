using Repositories.Models;
using PlatformRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork
    {
        private readonly DentalClinicPlatformContext _context;

        private GenericRepository<User, int>? _userRepository;
        private GenericRepository<Role, int>? _roleRepository;
        private GenericRepository<Service, int>? _serviceRepository;
        private GenericRepository<Clinic, int>? _clinicRepository;
        private GenericRepository<ClinicService, Guid>? _clinicServiceRepository;
        private GenericRepository<Booking, Guid>? _bookRepository;
        private GenericRepository<Medium, Guid>? _mediaRepository;
        private GenericRepository<Status, int>? _statusRepository;
        private GenericRepository<Slot, Guid>? _slotRepository;
        private GenericRepository<Result, Guid>? _resultRepository;
        private GenericRepository<Payment, Guid>? _paymentRepository;
        private GenericRepository<Message, Guid>? _messageRepository;
        private GenericRepository<PaymentType, int>? _paymentTypeRepository;

        public UnitOfWork(DentalClinicPlatformContext context)
        {
            _context = context;
        }

        public GenericRepository<Status, int> StatusRepository
        {
            get
            {
                if (_statusRepository == null) 
                {
                    this._statusRepository = new GenericRepository<Status, int>(_context);
                }

                return _statusRepository;
            }
        }

        public GenericRepository<User, int> UserRepository 
        {
            get
            {
                if (_userRepository == null)
                {
                    this._userRepository = new GenericRepository<User, int>(_context);
                }

                return this._userRepository;
            }
        }

        public GenericRepository<Role, int> RoleRepository
        {
            get
            {
                if ( _roleRepository == null)
                {
                    this._roleRepository = new GenericRepository<Role, int>(_context);
                }

                return this._roleRepository;
            }
        }

        public GenericRepository<Service, int> ServiceRepository
        {
            get
            {
                if (this._serviceRepository == null)
                {
                    this._serviceRepository = new GenericRepository<Service, int>(_context);
                }

                return _serviceRepository;
            }
        }

        public GenericRepository<Clinic, int> ClinicRepository
        {
            get 
            {
                if (_clinicRepository == null)
                {
                    this._clinicRepository = new GenericRepository<Clinic, int>(_context);  
                }

                return this._clinicRepository;
            }
        }

        public GenericRepository<ClinicService, Guid> ClinicServiceRepository
        {
            get 
            { 
                if (_clinicServiceRepository == null)
                {
                    this._clinicServiceRepository = new GenericRepository<ClinicService, Guid>(_context);
                }

                return _clinicServiceRepository;
            }
        }

        public GenericRepository<Booking, Guid> BookingRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    this._bookRepository = new GenericRepository<Booking, Guid>(_context);
                }

                return _bookRepository;
            }
        }

        public GenericRepository<Medium, Guid> MediaRepository
        {
            get
            {
                if (this._mediaRepository == null)
                {
                    this._mediaRepository = new GenericRepository<Medium, Guid>(_context);
                }

                return _mediaRepository;
            }
        }

        public GenericRepository<Slot, Guid> SlotRepository
        {
            get
            {
                if ( this._slotRepository == null)
                {
                    this._slotRepository = new GenericRepository<Slot, Guid>(_context);
                }

                return _slotRepository;
            }
        }

        public GenericRepository<Result, Guid> ResultRepository
        {
            get
            {
                if (this._resultRepository == null)
                {
                    this._resultRepository = new GenericRepository<Result, Guid>(_context);
                }

                return _resultRepository;
            }
        }

        public GenericRepository<Payment, Guid> PaymentRepository
        {
            get
            {
                if (this._paymentRepository == null)
                {
                    this._paymentRepository = new GenericRepository<Payment, Guid>(_context);
                }

                return _paymentRepository;
            }
        }

        public GenericRepository<PaymentType, int> PaymentTypeRepositoy
        {
            get
            {
                if (this._paymentTypeRepository == null)
                {
                    this._paymentTypeRepository = new GenericRepository<PaymentType, int>(_context);
                }

                return _paymentTypeRepository;
            }
        }

        public GenericRepository<Message, Guid> MessageRepository
        {
            get
            {
                if (this._messageRepository == null)
                {
                    this._messageRepository = new GenericRepository<Message, Guid>(_context);
                }

                return _messageRepository;
            }
        }

        /// <summary>
        ///  <para>Kiểm tra thông tin đăng nhập của một người dùng.</para>
        /// </summary>
        /// <param name="username">Tên tài khoản đăng nhập</param>
        /// <param name="password">Mật khẩu của tài khoản</param>
        /// <returns>Thông tin chi tiết của <see cref="User">người dùng</see> </returns>
        public User? Authenticate(string username, string password)
        {
            return UserRepository.context.Users.Where((user) => (user.Username == username && user.Password == password)).FirstOrDefault();
        }

        /// <summary>
        ///   <para>Kiểm tra thông tin về username và email để kiểm tra có thể tạo người dùng mới trong hệ thống hay không.</para>
        ///   <para>Trả về một chuỗi nêu lí do tại sao lại thất bại (nếu có).</para>
        /// </summary>
        /// <param name="username">Tên tài khoản cần kiểm tra</param>
        /// <param name="email">Email tài khoản cần kiểm tra</param>
        /// <param name="message">Tin nhắn đầu ra</param>
        /// <returns><see cref="bool">true</see> nếu có thể tạo một người dùng, <see cref="bool">false</see> nếu email hoặc username đã tồn tại.</returns>
        public bool CheckAvailability(string username, string email, out string message)
        {
            List<User> ExistanceList = UserRepository.context.Users.Where((user) => (user.Username == username || user.Email == email)).ToList(); ;

            foreach (User user in ExistanceList)
            {
                if (user.Username.Equals(username))
                {
                    message = "Account with this username is already existed";
                    return false;
                }

                if (user.Email.Equals(email))
                {
                    message = "Account with this email is already existed";
                    return false;
                }
            }

            message = "Account is available for creation";
            return true;
        }

        /// <summary>
        ///  <para>Kiểm tra xem người dùng có tồn tại trong hệ thống hay không dựa trên ID của họ trong hệ thống.</para>
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <param name="user">Đầu ra là một thông tin User nếu tìm thấy, không thì null</param>
        /// <returns>Trả về <see cref="bool">true</see> nếu có tồn tại người dùng với id này, không thì <see cref="bool">false</see>.</returns>
        public bool UserExists(int id, out User? user)
        {
            if (UserRepository.GetById(id) != null)
            {
                user = UserRepository.GetById(id);
                return true;
            };

            user = null;
            return false;
        }

        /// <summary>
        ///  <para>Tìm thông tin của một vai trò người dùng trong hệ thống.</para>
        /// </summary>
        /// <param name="roleName">Tên vai trò</param>
        /// <returns><see cref="Nullable">null</see> nếu không tồn tại <see cref="Role"/> nào có tên như input, nếu tồn tại thì trả về thông tin của <see cref="Role"/> đó.</returns>
        public Role? GetRoleByName(string roleName)
        {
            return _context.Roles.First(x => x.RoleName == roleName);
        }

        /// <summary>
        ///     <para>Tìm thông tin của người dùng dựa trên email.</para>
        ///     <para>Mỗi người dùng chỉ được liên kết một địa chỉ với một tài khoản nên có thể dễ dàng tìm kiếm thông tin cảu người đó.</para>
        /// </summary>
        /// <param name="email">Email của người dùng</param>
        /// <returns><see cref="Nullable">null</see> nếu người dùng không tồn tại, thông tin <see cref="User"/> nếu có.</returns>
        public User? GetUserWithEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        /// <summary>
        ///  <para>Kiếm thông tin về một trạng thái của vật thể trong database dựa trên tên của trạng thái.</para>
        /// </summary>
        /// <param name="statusName">Tên của trạng thái</param>
        /// <returns>thông tin trạng thái <see cref="Status"/> trong hệ thống nếu tồn tại, không thì <see cref="Nullable">null</see>.</returns>
        public Status? GetStatusByName(string statusName)
        {
            return _context.Statuses.First(x => x.StatusName == statusName);
        }

        /// <summary>
        ///  Lưu trạng thái hiện tại của context xuống database. (commit changes) <br/>
        ///  Nếu không gọi hàm này khi thay đổi thông tin thì tất cả thay đổi sẽ được đặt trong trạng thái "chờ"
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
