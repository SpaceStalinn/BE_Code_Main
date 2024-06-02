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


        public User? Authenticate(string username, string password)
        {
            List<User> tempt = UserRepository.context.Users.Where((user) => (user.Username == username && user.Password == password)).ToList();

            if (tempt.Count < 1)
            {
                return null;
            }
            else
            {
                return tempt[0];
            }
        }

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
